using System.Reflection;
using System;
using System.ComponentModel;

using System.Collections.Generic;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using store.Src.UserModule.DTO;
using store.Src.UserModule.Entity;
using store.Src.UserModule.Interface;
using store.Src.Utils.Common;
using Xunit;

using store.Src.Utils.Interface;
using store.Src.Utils;
using store.Src.Utils.Test;
using System.Diagnostics;


using Microsoft.Extensions.Logging;
using store.Src.AuthModule.DTO;
using store.Src.AuthModule.Interface;
using store.Src.AuthModule;


namespace store.Src.UserModule.Test
{


    public class UserControllerTest
    {
        private readonly UserController userController;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly IUploadFileService uploadFileService;
        private readonly User user;


        public UserControllerTest()
        {

            UpdateUserDtoValidator updateUserDtoValidator = new UpdateUserDtoValidator();
            LoginUserDtoValidator loginUserDtoValidator = new LoginUserDtoValidator();
            RegisterUserDtoValidator registerUserDtoValidation = new RegisterUserDtoValidator();
            UpdateUserPasswordDtoValidator updateUserPasswordDtoValidator = new UpdateUserPasswordDtoValidator();

            ConfigTest config = new ConfigTest();
            IDBHelper dbHelper = new DBHelper(config);
            IJwtService jwtService = new JwtService(config);
            this.userRepository = new UserRepository(dbHelper);
            this.userService = new UserService(userRepository);
            this.authService = new AuthService();
            this.uploadFileService = new UploadFileService();
            this.userController = new UserController(uploadFileService, userService, authService, loginUserDtoValidator, registerUserDtoValidation, updateUserDtoValidator, updateUserPasswordDtoValidator);

            this.user = new User();
            this.user.userId = Guid.NewGuid().ToString();
            this.user.username = TestHelper.randomString(10, RamdomStringType.LETTER_LOWER_CASE);
            this.user.password = "123456";
            this.userController.ViewData["user"] = user;
            this.userRepository.saveUser(user);
        }



        [Fact]
        public void passUpdate()
        {

            UpdateUserDto input = new UpdateUserDto("helllo123", "hello@gmail.com", "0901212099", "anywhere");
            this.userController.updateUser(input);
            User userUpdate = this.userService.getUserById(this.user.userId);
            Assert.Equal("helllo123", userUpdate.name);
            Assert.Equal("hello@gmail.com", userUpdate.email);
            Assert.Equal("0901212099", userUpdate.phone);
            Assert.Equal("anywhere", userUpdate.address);
        }

        [Fact]
        public void FailedInputUpdate()
        {
            UpdateUserDto input = new UpdateUserDto("", "hello@gmail.com", "0901212099", "anywhere");
            var res = this.userController.updateUser(input);
            Assert.Equal(400, res.StatusCode);
        }
        [Fact]
        public void passUpdatePassword()
        {
            UpdateUserPasswordDto input = new UpdateUserPasswordDto("123456789", "123", "123");
            var res = this.userController.updateUserPassword(input);
            Assert.Null(res.StatusCode);
        }
    }
}