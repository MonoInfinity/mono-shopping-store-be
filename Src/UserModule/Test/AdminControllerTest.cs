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
    public class AdminControllerTest
    {

        private readonly AdminController adminController;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly IAdminService adminService;
        private readonly User user;


        public AdminControllerTest()
        {
            UpdateEmployeeDtoValidator updateEmployeeDtoValidator = new UpdateEmployeeDtoValidator();
            UpdateUserDtoValidator updateUserDtoValidator = new UpdateUserDtoValidator();
            LoginUserDtoValidator loginUserDtoValidator = new LoginUserDtoValidator();
            RegisterUserDtoValidator registerUserDtoValidation = new RegisterUserDtoValidator();
            ConfigTest config = new ConfigTest();
            IDBHelper dbHelper = new DBHelper(config);
            IJwtService jwtService = new JwtService(config);
            this.userRepository = new UserRepository(dbHelper);
            this.userService = new UserService(userRepository);
            this.adminService = new AdminService(userRepository);
            this.adminController = new AdminController(userService, adminService, updateEmployeeDtoValidator);
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
            var result = this.adminController.listAllUser(12, 0, "", "");
            var res = (Dictionary<string, object>)result.Value;
            var user = res["data"] as List<User>;

            Assert.Equal(12, user.Count);
        }

        [Fact]
        public void PassGetAllUserPageSize()
        {
            var result = this.adminController.listAllUser(20, 0, "", "");
            var res = (Dictionary<string, object>)result.Value;
            var user = res["data"] as List<User>;

            Assert.Equal(20, user.Count);
        }



        [Fact]
        public void PassGetAllUserPageLessThanZero()
        {
            var result = this.adminController.listAllUser(12, -1, "", "");
            var res = (Dictionary<string, object>)result.Value;
            var user = res["data"] as List<User>;

            Assert.Equal(0, user.Count);
        }

        [Fact]
        public void FailUpdateAdmin()
        {
            UpdateEmployeeDto input = new UpdateEmployeeDto(this.user.userId, 4, 112, 1);
            var res = this.adminController.updateEmployee(input);
            Assert.Equal(406, res.StatusCode);
        }

        [Fact]
        public void FailUpdateUser()
        {
            UpdateEmployeeDto input = new UpdateEmployeeDto("08df8c88-49e0-4888-9cab-15aff6ea547", 1, 1, 1);
            var res = this.adminController.updateEmployee(input);
            Assert.Equal(404, res.StatusCode);
        }
        [Fact]
        public void failUpdateCustomerSalary()
        {
            UpdateEmployeeDto input = new UpdateEmployeeDto(this.user.userId, 1, 1000, 1);
            var res = this.adminController.updateEmployee(input);
            Assert.Equal(403, res.StatusCode);
        }
    }
}