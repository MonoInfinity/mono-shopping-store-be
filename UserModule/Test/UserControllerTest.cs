using System.Reflection;
using System;
using System.ComponentModel;

using System.Collections.Generic;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using store.UserModule.DTO;
using store.UserModule.Entity;
using store.UserModule.Interface;
using store.Utils.Common;
using Xunit;

using store.Utils.Interface;
using store.Utils;
using store.Utils.Test;
using System.Diagnostics;


using Microsoft.Extensions.Logging;
using store.AuthModule.DTO;
using store.AuthModule.Interface;
using store.AuthModule;


namespace store.UserModule.Test
{


    public class UserControllerTest
    {
        private readonly UserController userController;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly User user;


        public UserControllerTest()
        {

            UpdateUserDtoValidator updateUserDtoValidator = new UpdateUserDtoValidator();
            LoginUserDtoValidator loginUserDtoValidator = new LoginUserDtoValidator();
            RegisterUserDtoValidator registerUserDtoValidation = new RegisterUserDtoValidator();
            ConfigTest config = new ConfigTest();
            IDBHelper dbHelper = new DBHelper(config);
            IJwtService jwtService = new JwtService(config);
            this.userRepository = new UserRepository(dbHelper);
            this.userService = new UserService(userRepository);
            this.authService = new AuthService();
            this.userController = new UserController(userService, authService, loginUserDtoValidator, registerUserDtoValidation, updateUserDtoValidator);

            this.user = new User();
            this.user.userId = Guid.NewGuid().ToString();
            this.user.username = TestHelper.randomString(10, RamdomStringType.LETTER_LOWER_CASE);
            this.user.password = "123456";
            this.userRepository.saveUser(user);
        }



        [Fact]
        public void passUpdate()
        {
            UpdateUserDto input = new UpdateUserDto(this.user.username, "helllo123", "hello@gmail.com", "0901212099", "anywhere");
            var res = this.userController.updateUser(input);
            Assert.Null(res.StatusCode);
        }

        [Fact]
        public void FailedInputUpdate()
        {
            UpdateUserDto input = new UpdateUserDto(this.user.username, "", this.user.email, this.user.phone, this.user.address);
            var res = this.userController.updateUser(input);


            Assert.Equal(400, res.StatusCode);
        }

        [Fact]
        public void FailedNotFoundUserUpdate()
        {
            UpdateUserDto input = new UpdateUserDto("abcfdff", this.user.name, this.user.email, this.user.phone, this.user.address);
            var res = this.userController.updateUser(input);


            Assert.Equal(400, res.StatusCode);
        }
    }
}