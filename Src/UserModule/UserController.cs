using Microsoft.AspNetCore.Mvc;
using store.Src.AuthModule.DTO;
using store.Src.AuthModule.Interface;
using store.Src.UserModule.DTO;
using store.Src.UserModule.Entity;
using store.Src.UserModule.Interface;
using store.Src.Utils.Common;
using store.Src.AuthModule;
using store.Src.Utils.Validator;
using store.Src.Utils.Interface;
using store.Src.Utils;
using System;
using store.Src.Providers.Smail.Interface;
using static store.Src.Utils.Locale.CustomLanguageValidator;

namespace store.Src.UserModule
{
    [ApiController]
    [Route("/api/user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class UserController : Controller, IUserController
    {
        private readonly ISmailService smailService;
        private readonly IUploadFileService uploadFileService;
        private readonly IUserService userService;
        private readonly IAuthService authService;
        public UserController(ISmailService smailService, IUploadFileService uploadFileService, IUserService userService, IAuthService authService)
        {
            this.smailService = smailService;
            this.uploadFileService = uploadFileService;
            this.userService = userService;
            this.authService = authService;
        }

        [HttpGet("")]
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

            if (body.avatarUrl.Length != 0)
            {
                userUpdate.avatarUrl = body.avatarUrl;
            }
            else
            {
                userUpdate.avatarUrl = user.avatarUrl;
            }


            bool isUpdated = userService.updateUser(userUpdate);
            if (!isUpdated)
            {
                res.setErrorMessage(ErrorMessageKey.Error_UpdateFail);
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            res.setMessage(MessageKey.Message_UpdateSuccess);
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("password")]
        [ValidateFilterAttribute(typeof(UpdateUserPasswordDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult updateUserPassword([FromBody] UpdateUserPasswordDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            var user = this.ViewData["user"] as User;

            bool isMatchPassword = this.authService.comparePassword(body.password, user.password);
            if (!isMatchPassword)
            {
                res.setErrorMessage(ErrorMessageKey.Error_Wrong, "password");
                return new BadRequestObjectResult(res.getResponse());
            }

            user.password = this.authService.hashingPassword(body.newPassword);

            bool isUpdate = this.userService.updateUserPassword(user.userId, user.password);
            if (!isUpdate)
            {
                res.setErrorMessage(ErrorMessageKey.Error_UpdateFail);
                return new BadRequestObjectResult(res.getResponse());
            }

            res.setMessage(MessageKey.Message_UpdateSuccess);
            return new ObjectResult(res.getResponse());
        }
    }
}