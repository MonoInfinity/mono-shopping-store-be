

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
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        private readonly RegisterUserDtoValidator registerUserDtoValidator;
        private readonly UpdateUserPasswordDtoValidater updateUserPasswordDtoValidator;

        public UserController(IUserService userService, LoginUserDtoValidator loginUserDtoValidator, RegisterUserDtoValidator registerUserDtoValidator, UpdateUserPasswordDtoValidater updateUserPasswordDtoValidator)
        {
            this.userService = userService;
            this.loginUserDtoValidator = loginUserDtoValidator;
            this.registerUserDtoValidator = registerUserDtoValidator;
            this.updateUserPasswordDtoValidator = updateUserPasswordDtoValidator;
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



            bool isMatchPassword = this.userService.comparePassword(body.password, user.password);
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
            insertedUser.password = this.userService.hashingPassword(body.password);
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

        [HttpPut("password")]
        public IDictionary<string, object> updateUserPassword([FromBody] UpdateUserPasswordDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();

            ValidationResult result = this.updateUserPasswordDtoValidator.Validate(body);
            res.mapDetails(result);

            if (!result.IsValid)
            {
                return res.getResponse();
            }

            User user = this.userService.getUserByUsername(body.username);
            if (user == null)
            {
                res.setErrorMessage("User not found");
                return res.getResponse();
            }

            bool isMatchPassword = body.newPassword.Equals(body.confirmPassword);

            if (!isMatchPassword)
            {
                res.setErrorMessage("Password or confirm-password is wrong");
                return res.getResponse();
            }
            bool isMatchOldPassword = this.userService.comparePassword(body.newPassword, user.password);

            if (isMatchOldPassword)
            {
                res.setErrorMessage("New password can't be duplicate with old password");
                return res.getResponse();
            }
            user.password = this.userService.hashingPassword(body.newPassword);

            bool isUpdate = this.userService.updateUserPassword(user.username, user.password);

            if (!isUpdate)
            {
                res.setErrorMessage("Fail to update user password");
                return res.getResponse();
            }

            res.data = user;
            return res.getResponse();

        }
    }
}