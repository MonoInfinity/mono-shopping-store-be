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
    public class AdminControllerTest
    {

        private readonly AdminController adminController;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly IAdminService adminService;
        private readonly User user;


        public AdminControllerTest()
        {

            UpdateUserDtoValidator updateUserDtoValidator = new UpdateUserDtoValidator();
            LoginUserDtoValidator loginUserDtoValidator = new LoginUserDtoValidator();
            RegisterUserDtoValidator registerUserDtoValidation = new RegisterUserDtoValidator();
            ConfigTest config = new ConfigTest();
            IDBHelper dbHelper = new DBHelper(config);
            IJwtService jwtService = new JwtService(config);
            this.userRepository = new UserRepository(dbHelper);
            this.userService = new UserService(userRepository);
            this.adminService = new AdminService(userRepository);
            this.adminController = new AdminController(userService, adminService);
            this.user = new User();
            this.user.userId = Guid.NewGuid().ToString();
            this.user.username = TestHelper.randomString(10, RamdomStringType.LETTER_LOWER_CASE);
            this.user.password = "123456";
            this.user.role = UserRole.MANAGER;
            this.userRepository.saveUser(this.user);
            this.adminController.ViewData["user"] = this.user;
        }

        [Fact]
        public void PassGetAllUser()
        {
            var result = this.adminController.listAllUser(12, 0);
            var res = (Dictionary<string, object>)result.Value;
            var user = res["data"] as List<User>;

            Assert.Equal(12, user.Count);
        }

        [Fact]
        public void PassGetAllUserPageSize()
        {
            var result = this.adminController.listAllUser(20, 0);
            var res = (Dictionary<string, object>)result.Value;
            var user = res["data"] as List<User>;

            Assert.Equal(20, user.Count);
        }



        [Fact]
        public void PassGetAllUserPageLessThanZero()
        {
            var result = this.adminController.listAllUser(12, -1);
            var res = (Dictionary<string, object>)result.Value;
            var user = res["data"] as List<User>;

            Assert.Equal(0, user.Count);
        }

    }
}