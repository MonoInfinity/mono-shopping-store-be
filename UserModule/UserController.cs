using System;
using System.Collections.Generic;
using FluentValidation.Results;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using mono_store_be.Utils.Interface;
using store.AuthModule.DTO;
using store.AuthModule.Interface;
using store.UserModule.DTO;
using store.UserModule.Entity;
using store.UserModule.Interface;
using store.Utils.Common;
using System.Diagnostics;


namespace store.UserModule
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : IUserController
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        private readonly RegisterUserDtoValidator registerUserDtoValidator;
        private readonly UpdateUserDtoValidator updateUserDtoValidator;
        public UserController(IUserService userService, IAuthService authService, LoginUserDtoValidator loginUserDtoValidator, RegisterUserDtoValidator registerUserDtoValidator, UpdateUserDtoValidator updateUserDtoValidator)
        {
            // this.loggerr = loggerr;
            this.userService = userService;
            this.updateUserDtoValidator = updateUserDtoValidator;
            this.loginUserDtoValidator = loginUserDtoValidator;
            this.registerUserDtoValidator = registerUserDtoValidator;
            this.authService = authService;
        }

        [HttpPost("update")]
        public IDictionary<string, Object> updateUser([FromBody] UpdateUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            ValidationResult result = this.updateUserDtoValidator.Validate(body);
            res.mapDetails(result);

            if (!result.IsValid)
            {
                return res.getResponse();
            }


            User user = this.userService.getUserByUsername(body.username);

            if (user == null)
            {
                res.setErrorMessage("User with the given id was not found");
                return res.getResponse();
            }
            else
            {
                user = new User();
                user.username = body.username;
                user.name = body.newName;
                user.email = body.newEmail;
                user.phone = body.newPhone;
                user.address = body.newAddress;
            }

            bool isUpdated = userService.updateUser(user);
            if (!isUpdated)
            {
                res.setErrorMessage("Update fail");
                return res.getResponse();
            }

            res.data = user;
            return res.getResponse();
        }
    }
}