

using System;
using System.Collections.Generic;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using store.UserModule.DTO;
using store.UserModule.Entity;
using store.UserModule.Interface;
using store.Utils.Common;
namespace store.UserModule
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : IUserController
    {

        private readonly IUserService userService;
        private readonly UpdateUserDtoValidator updateUserDtoValidator;
        public UserController(IUserService userService, UpdateUserDtoValidator updateUserDtoValidator)
        {
            this.userService = userService;
            this.updateUserDtoValidator = updateUserDtoValidator;
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

            User user = userService.getUserByUsername(body.username);
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