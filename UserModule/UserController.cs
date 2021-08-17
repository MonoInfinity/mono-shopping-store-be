

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
        private readonly UpdateUserDtoValidator updateUserDtoValidator;
        public UserController(IUserService userService, LoginUserDtoValidator loginUserDtoValidator, RegisterUserDtoValidator registerUserDtoValidator, UpdateUserDtoValidator updateUserDtoValidator)
        {
            this.userService = userService;
            this.loginUserDtoValidator = loginUserDtoValidator;
            this.registerUserDtoValidator = registerUserDtoValidator;
            this.updateUserDtoValidator = updateUserDtoValidator;
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
                Console.WriteLine(user);
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
            Console.WriteLine(user);
            Console.WriteLine(isUpdated);
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