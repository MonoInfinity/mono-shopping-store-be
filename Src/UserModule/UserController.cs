using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using store.Src.AuthModule.DTO;
using store.Src.AuthModule.Interface;
using store.Src.UserModule.DTO;
using store.Src.UserModule.Entity;
using store.Src.UserModule.Interface;
using store.Src.Utils.Common;
using store.Src.AuthModule;
using System.Diagnostics;
using store.Src.Utils.Validator;
using Microsoft.AspNetCore.Http;
using store.Src.Utils.Interface;
using store.Src.Utils;
using System;
using store.Src.Providers.Smail.Interface;

namespace store.Src.UserModule
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : Controller, IUserController
    {
        private readonly ISmailService smailService;
        private readonly IUploadFileService uploadFileService;
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        private readonly RegisterUserDtoValidator registerUserDtoValidator;
        private readonly UpdateUserDtoValidator updateUserDtoValidator;
        private readonly UpdateUserPasswordDtoValidator updateUserPasswordDtoValidator;
        public UserController(ISmailService smailService, IUploadFileService uploadFileService, IUserService userService, IAuthService authService, LoginUserDtoValidator loginUserDtoValidator, RegisterUserDtoValidator registerUserDtoValidator, UpdateUserDtoValidator updateUserDtoValidator, UpdateUserPasswordDtoValidator updateUserPasswordDtoValidator)
        {
            this.smailService = smailService;
            this.uploadFileService = uploadFileService;
            this.userService = userService;
            this.updateUserDtoValidator = updateUserDtoValidator;
            this.updateUserPasswordDtoValidator = updateUserPasswordDtoValidator;
            this.loginUserDtoValidator = loginUserDtoValidator;
            this.registerUserDtoValidator = registerUserDtoValidator;
            this.authService = authService;
        }

        [HttpGet("")]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult getUser()
        {
            ServerResponse<User> res = new ServerResponse<User>();
            var user = this.ViewData["user"] as User;
            user.password = "";
            res.data = user;
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("")]
        [ValidateFilterAttribute(typeof(UpdateUserDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult updateUser([FromBody] UpdateUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            var user = this.ViewData["user"] as User;

            User userUpdate = new User();
            userUpdate.userId = user.userId;
            userUpdate.name = body.name;
            userUpdate.email = body.email;
            userUpdate.phone = body.phone;
            userUpdate.address = body.address;
            if (body.avatarUrl != null)
            {
                userUpdate.avatarUrl = body.avatarUrl;
            }


            bool isUpdated = userService.updateUser(userUpdate);
            if (!isUpdated)
            {
                res.setErrorMessage("Update fail");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            res.setMessage("Update User successfully");
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("password")]
        [ValidateFilterAttribute(typeof(UpdateUserPasswordDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult updateUserPassword([FromBody] UpdateUserPasswordDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            var user = this.ViewData["user"] as User;

            bool isMatchPassword = this.authService.comparePassword(body.password, user.password);

            if (!isMatchPassword)
            {
                res.setErrorMessage("Password is wrong");
                return new BadRequestObjectResult(res.getResponse());
            }

            user.password = this.authService.hashingPassword(body.newPassword);

            bool isUpdate = this.userService.updateUserPassword(user.userId, user.password);

            if (!isUpdate)
            {
                res.setErrorMessage("Fail to update user password");
                return new BadRequestObjectResult(res.getResponse());
            }

            res.setMessage("Update User password successfully");
            return new ObjectResult(res.getResponse());
        }
    }
}