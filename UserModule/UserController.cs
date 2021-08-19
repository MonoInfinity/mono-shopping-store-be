

using System;
using System.Collections.Generic;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using store.Utils.Interface;
using store.AuthModule.DTO;
using store.AuthModule.Interface;
using store.UserModule.DTO;
using store.UserModule.Entity;
using store.UserModule.Interface;
using store.Utils.Common;
using store.AuthModule;
namespace store.UserModule
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : Controller, IUserController
    {

        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        private readonly RegisterUserDtoValidator registerUserDtoValidator;
        private readonly UpdateUserDtoValidator updateUserDtoValidator;
        public UserController(IUserService userService, IAuthService authService, LoginUserDtoValidator loginUserDtoValidator, RegisterUserDtoValidator registerUserDtoValidator, UpdateUserDtoValidator updateUserDtoValidator)
        {
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
            var user = this.ViewData["user"] as User;
            user.password = "";
            return new ObjectResult(user);
        }

        [HttpPost("update")]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult updateUser([FromBody] UpdateUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            ValidationResult result = this.updateUserDtoValidator.Validate(body);
            res.mapDetails(result);

            if (!result.IsValid)
            {
                return new BadRequestObjectResult(res.getResponse());
            }

            User user = userService.getUserByUsername(body.username);
            if (user == null)
            {
                res.setErrorMessage("User with the given id was not found");
                return new BadRequestObjectResult(res.getResponse());
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
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            res.data = user;
            return new ObjectResult(res.getResponse());
        }
    }
}