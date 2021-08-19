

using System;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using store.AuthModule.DTO;
using store.UserModule.Entity;
using store.Utils.Common;
using store.UserModule.Interface;
using store.AuthModule.Interface;
using store.Utils.Interface;
using System.Net.Http;
using System.Net.Http.Headers;

namespace store.AuthModule
{


    [ApiController]
    [Route("/api/auth")]
    public class AuthController : Controller, IAuthController
    {

        private readonly IJwtService jwtService;
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        private readonly RegisterUserDtoValidator registerUserDtoValidator;

        public AuthController(IUserService userService, IAuthService authService, IJwtService jwtService, LoginUserDtoValidator loginUserDtoValidator, RegisterUserDtoValidator registerUserDtoValidator)
        {
            this.userService = userService;
            this.loginUserDtoValidator = loginUserDtoValidator;
            this.registerUserDtoValidator = registerUserDtoValidator;
            this.authService = authService;
            this.jwtService = jwtService;
        }

        [HttpPost("login")]

        public ObjectResult loginUser([FromBody] LoginUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            ValidationResult result = this.loginUserDtoValidator.Validate(body);
            res.mapDetails(result);

            if (!result.IsValid)
            {
                return new BadRequestObjectResult(res.getResponse());
            }


            User existedUser = this.userService.getUserByUsername(body.username);


            if (existedUser == null)
            {
                res.setErrorMessage("Username or password is wrong");
                return new BadRequestObjectResult(res.getResponse());
            }



            bool isMatchPassword = this.authService.comparePassword(body.password, existedUser.password);
            if (!isMatchPassword)
            {
                res.setErrorMessage("Username or password is wrong");
                return new BadRequestObjectResult(res.getResponse());

            }

            res.data = existedUser;
            var token = this.jwtService.GenerateToken(existedUser.userId);
            var resp = new HttpResponseMessage();
            var authCookie = new CookieHeaderValue("auth-token", token);
            authCookie.Expires = DateTime.Now.AddDays(30);

            resp.Headers.AddCookies(new CookieHeaderValue[] { authCookie });
            return new ObjectResult(resp);
        }

        [HttpPost("register")]
        public ObjectResult registerUser(RegisterUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();

            ValidationResult result = this.registerUserDtoValidator.Validate(body);
            res.mapDetails(result);

            if (!result.IsValid)
            {
                return new BadRequestObjectResult(res.getResponse());
            }

            User user = this.userService.getUserByUsername(body.username);
            if (user != null)
            {
                res.setErrorMessage("Username is already exist");
                return new BadRequestObjectResult(res.getResponse());
            }

            User insertedUser = new User();
            insertedUser.userId = Guid.NewGuid().ToString();
            insertedUser.name = body.name;
            insertedUser.username = body.username;
            insertedUser.password = this.authService.hashingPassword(body.password);
            insertedUser.email = body.email;
            insertedUser.phone = body.phone;
            insertedUser.address = body.address;
            insertedUser.createDate = DateTime.Now;
            insertedUser.role = UserRole.CUSTOMER;

            bool isInserted = this.userService.saveUser(insertedUser);
            if (!isInserted)
            {
                res.setErrorMessage("Fail to save new user");
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.data = insertedUser;
            return new ObjectResult(res.getResponse());
        }
    }
}