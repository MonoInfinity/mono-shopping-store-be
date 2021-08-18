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
using store.UserModule;

using store.Utils.Interface;
using store.Utils;
using store.Utils.Test;
using store.AuthModule.Interface;
using store.AuthModule.DTO;
using store.AuthModule;

using Microsoft.Extensions.Logging;
using mono_store_be.Utils;
using mono_store_be.Utils.Interface;
using mono_store_be.AuthModule;

namespace store.AuthModule.Test
{
    public class AuthControllerTest
    {
        private readonly IAuthController authController;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly User user;



        public AuthControllerTest()
        {

            LoginUserDtoValidator loginUserDtoValidator = new LoginUserDtoValidator();
            RegisterUserDtoValidator registerUserDtoValidator = new RegisterUserDtoValidator();
            UpdateUserDtoValidator updateUserDtoValidator = new UpdateUserDtoValidator();
            ConfigTest config = new ConfigTest();
            IDBHelper dbHelper = new DBHelper(config);
            IJwtService jwtService = new JwtService(config);
            this.userRepository = new UserRepository(dbHelper);
            this.userService = new UserService(userRepository);
            this.authService = new AuthService(jwtService);
            this.authController = new AuthController(userService, authService, loginUserDtoValidator, registerUserDtoValidator);

            this.user = new User();
            this.user.userId = Guid.NewGuid().ToString();
            this.user.username = TestHelper.randomString(10, RamdomStringType.LETTER_LOWER_CASE);
            this.user.password = this.authService.hashingPassword("123456789");

            this.userRepository.saveUser(user);
        }

        [Fact]
        public void passLogin()
        {
            LoginUserDto input = new LoginUserDto(this.user.username, "123456789");
            var res = this.authController.loginUser(input);
            User user = res["data"] as User;


            Assert.NotNull(user);
        }

        [Fact]
        public void FailedInputLogin()
        {
            LoginUserDto input = new LoginUserDto("", "123");
            var res = this.authController.loginUser(input);


            Assert.Null(res["data"]);
            Assert.NotNull(res["details"]);
        }

        [Fact]
        public void FailedNotFoundUserLogin()
        {
            LoginUserDto input = new LoginUserDto("12361632", "123");
            var res = this.authController.loginUser(input);


            Assert.Null(res["data"]);
            Assert.NotNull(res["details"]);
        }


    }
}