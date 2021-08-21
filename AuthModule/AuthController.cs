using System;
using Microsoft.AspNetCore.Mvc;
using store.AuthModule.DTO;
using store.UserModule.Entity;
using store.Utils.Common;
using store.UserModule.Interface;
using store.AuthModule.Interface;
using store.Utils.Interface;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using store.Utils.Validator;
using store.Utils;

namespace store.AuthModule
{


    [ApiController]
    [Route("/api/auth")]
    public class AuthController : Controller, IAuthController
    {

        private readonly IJwtService jwtService;
        private readonly IUserService userService;
        private readonly IAuthService authService;


        public AuthController(IUserService userService, IAuthService authService, IJwtService jwtService)
        {
            this.userService = userService;
            this.authService = authService;
            this.jwtService = jwtService;
        }

        [HttpPost("login")]
        [ValidateFilterAttribute(typeof(LoginUserDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult loginUser([FromBody] LoginUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();



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

            res.setMessage("Login user successfully");
            var token = this.jwtService.GenerateToken(existedUser.userId);



            this.HttpContext.Response.Cookies.Append("auth-token", token, new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(30),
                SameSite = SameSiteMode.None,
                Secure = true

            });
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("register")]
        [ValidateFilterAttribute(typeof(RegisterUserDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult registerUser(RegisterUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();
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
            insertedUser.createDate = DateTime.Now.ToShortDateString();
            insertedUser.role = UserRole.CUSTOMER;
            insertedUser.avatarUrl = Helper.randomUserAvatar();
          

            bool isInserted = this.userService.saveUser(insertedUser);
            if (!isInserted)
            {
                res.setErrorMessage("Fail to save new user");
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.data = insertedUser;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("logout")]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult logoutUser()
        {
            var resp = new HttpResponseMessage();
            var authCookie = new CookieHeaderValue("auth-token", "");
            authCookie.Expires = DateTime.Now.AddDays(-1);
            resp.Headers.AddCookies(new CookieHeaderValue[] { authCookie });
            return new ObjectResult(resp);
        }
    }
}