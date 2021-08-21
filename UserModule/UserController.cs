using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using store.AuthModule.DTO;
using store.AuthModule.Interface;
using store.UserModule.DTO;
using store.UserModule.Entity;
using store.UserModule.Interface;
using store.Utils.Common;
using store.AuthModule;
using System.Diagnostics;
using store.Utils.Validator;
using Microsoft.AspNetCore.Http;
using store.Utils.Interface;
using store.Utils;
using System;

namespace store.UserModule
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : Controller, IUserController
    {
        private readonly IUploadFileService uploadFileService;
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        private readonly RegisterUserDtoValidator registerUserDtoValidator;
        private readonly UpdateUserDtoValidator updateUserDtoValidator;
        public UserController(IUploadFileService uploadFileService, IUserService userService, IAuthService authService, LoginUserDtoValidator loginUserDtoValidator, RegisterUserDtoValidator registerUserDtoValidator, UpdateUserDtoValidator updateUserDtoValidator)
        {
            // this.loggerr = loggerr;
            this.uploadFileService = uploadFileService;
            this.userService = userService;
            this.updateUserDtoValidator = updateUserDtoValidator;
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

        [HttpPut("update")]
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

            bool isUpdated = userService.updateUser(userUpdate);
            if (!isUpdated)
            {
                res.setErrorMessage("Update fail");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            res.setMessage("Update User successfully");
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("avatar")]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult updateAvatar(IFormFile file)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            var user = this.ViewData["user"] as User;

            if(file == null){
                res.setErrorMessage("Not found");
                return new BadRequestObjectResult(res.getResponse());
            }

            if(!this.uploadFileService.checkFileExtension(file, UploadFileService.imageExtension)){
                res.setErrorMessage("Not support this extension file. Please select png, jpg, jpeg");
                return new BadRequestObjectResult(res.getResponse());
            }
            if(!this.uploadFileService.checkFileSize(file, 1)){
                res.setErrorMessage("File is too big");
                return new BadRequestObjectResult(res.getResponse());
            }

            var avatarUrl = this.uploadFileService.upload(file);
            if(avatarUrl == null){
                res.setErrorMessage("Fail to upload file");
                return new BadRequestObjectResult(res.getResponse());
            }

            user.avatarUrl = avatarUrl;
            this.userService.updateUser(user);
            res.setMessage("Update avatar successfully");
            return new ObjectResult(res.getResponse());
        }
    }
}