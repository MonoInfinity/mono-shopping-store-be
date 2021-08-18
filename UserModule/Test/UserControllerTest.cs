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


using Microsoft.Extensions.Logging;
namespace store.UserModule.Test
{


    public class UserControllerTest : IDisposable
    {

        private readonly UserController userController;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly User user;


        public void Dispose() { }
        public UserControllerTest()
        {

            UpdateUserDtoValidator updateUserDtoValidator = new UpdateUserDtoValidator();
            ConfigTest config = new ConfigTest();
            IDBHelper dbHelper = new DBHelper(config);
            this.userRepository = new UserRepository(dbHelper);
            this.userService = new UserService(userRepository);
            this.userController = new UserController(userService, updateUserDtoValidator);

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
            User user = res["data"] as User;


            Assert.NotNull(user);
        }

        [Fact]
        public void FailedInputUpdate()
        {
            UpdateUserDto input = new UpdateUserDto(this.user.username, "", this.user.email, this.user.phone, this.user.address);
            var res = this.userController.updateUser(input);


            Assert.Null(res["data"]);
            Assert.NotNull(res["details"]);
        }

        [Fact]
        public void FailedNotFoundUserUpdate()
        {
            UpdateUserDto input = new UpdateUserDto("abcfdff", this.user.name, this.user.email, this.user.phone, this.user.address);
            var res = this.userController.updateUser(input);


            Assert.Null(res["data"]);
            Assert.NotNull(res["details"]);
        }
    }
}