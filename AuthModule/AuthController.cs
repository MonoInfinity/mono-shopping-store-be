

using System;
using System.Collections.Generic;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using store.AuthModule.DTO;
using store.UserModule.Entity;
using store.Utils.Common;
using store.UserModule.Interface;
using store.AuthModule.Interface;

namespace store.AuthModule
{


    [ApiController]
    [Route("/api/auth")]
    public class AuthController : IAuthController
    {


        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        private readonly RegisterUserDtoValidator registerUserDtoValidator;

        public AuthController(IUserService userService, IAuthService authService, LoginUserDtoValidator loginUserDtoValidator, RegisterUserDtoValidator registerUserDtoValidator)
        {
            this.userService = userService;
            this.loginUserDtoValidator = loginUserDtoValidator;
            this.registerUserDtoValidator = registerUserDtoValidator;
            this.authService = authService;

        }

        [HttpPost("login")]
        public IDictionary<string, Object> loginUser([FromBody] LoginUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();

            ValidationResult result = this.loginUserDtoValidator.Validate(body);
            res.mapDetails(result);

            if (!result.IsValid)
            {
                return res.getResponse();
            }


            User user = this.userService.getUserByUsername(body.username);


            if (user == null)
            {
                res.setErrorMessage("Username or password is wrong");
                return res.getResponse();
            }



            bool isMatchPassword = this.authService.comparePassword(body.password, user.password);
            if (!isMatchPassword)
            {
                res.setErrorMessage("Username or password is wrong");
                return res.getResponse();
            }

            res.data = user;
            return res.getResponse();
        }

        [HttpPost("register")]
        public IDictionary<string, object> registerUser(RegisterUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();

            ValidationResult result = this.registerUserDtoValidator.Validate(body);
            res.mapDetails(result);

            if (!result.IsValid)
            {
                return res.getResponse();
            }

            User user = this.userService.getUserByUsername(body.username);
            if (user != null)
            {
                res.setErrorMessage("Username is already exist");
                return res.getResponse();
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
                return res.getResponse();
            }
            res.data = insertedUser;
            return res.getResponse();
        }
    }
}