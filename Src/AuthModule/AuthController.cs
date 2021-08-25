using System;
using Microsoft.AspNetCore.Mvc;
using store.Src.AuthModule.DTO;
using store.Src.UserModule.Entity;
using store.Src.Utils.Common;
using store.Src.UserModule.Interface;
using store.Src.AuthModule.Interface;
using store.Src.Utils.Interface;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using store.Src.Utils.Validator;
using store.Src.Utils;
using FluentValidation.Results;
using FluentValidation.Resources;
using System.Globalization;
using FluentValidation;
using static store.Src.Utils.Locale.CustomLanguageValidator;

namespace store.Src.AuthModule
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
                res.setErrorMessage(ErrorMessageKey.Error_LoginFail);
                return new BadRequestObjectResult(res.getResponse());
            }



            bool isMatchPassword = this.authService.comparePassword(body.password, existedUser.password);
            if (!isMatchPassword)
            {
                res.setErrorMessage(ErrorMessageKey.Error_LoginFail);
                return new BadRequestObjectResult(res.getResponse());

            }

            var token = this.jwtService.GenerateToken(existedUser.userId);

            this.HttpContext.Response.Cookies.Append("auth-token", token, new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(30),
                SameSite = SameSiteMode.None,
                Secure = true

            });

            res.setMessage(MessageKey.Message_LoginSuccess);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("register")]
        [ValidateFilterAttribute(typeof(RegisterUserDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult registerUser([FromBody] RegisterUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            User user = this.userService.getUserByUsername(body.username);
            if (user != null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_UsernameExist, "username");
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
                res.setErrorMessage(ErrorMessageKey.Error_FailToSaveUser);
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.data = insertedUser;
            res.setMessage(MessageKey.Message_RegisterSuccess);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("logout")]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult logoutUser()
        {
            ServerResponse<User> res = new ServerResponse<User>();
            this.HttpContext.Response.Cookies.Append("auth-token", "", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(-1),
                SameSite = SameSiteMode.None,
                Secure = true

            });
            res.setMessage(MessageKey.Message_LogoutSuccess);
            return new ObjectResult(res.getResponse());
        }
    }
}