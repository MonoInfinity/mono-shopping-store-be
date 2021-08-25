using System;
using System.Collections.Generic;
using FluentValidation.Results;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using store.Src.Utils.Interface;
using store.Src.AuthModule.DTO;
using store.Src.AuthModule.Interface;
using store.Src.UserModule.DTO;
using store.Src.UserModule.Entity;
using store.Src.UserModule.Interface;
using store.Src.Utils.Common;
using store.Src.AuthModule;
using System.Diagnostics;



namespace store.Src.UserModule
{

    [ApiController]
    [Route("/api/admin")]
    [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER, UserRole.OWNER })]
    [ServiceFilter(typeof(AuthGuard))]
    public class AdminController : Controller, IAdminController
    {

        private readonly IUserService userService;

        private readonly IAdminService adminService;
        private readonly UpdateEmployeeDtoValidator updateEmployeeDtoValidator;

        public AdminController(IUserService userService, IAdminService adminService, UpdateEmployeeDtoValidator updateEmployeeDtoValidator)
        {

            this.userService = userService;
            this.adminService = adminService;
            this.updateEmployeeDtoValidator = updateEmployeeDtoValidator;

        }


        [HttpGet("user/all")]
        public ObjectResult listAllUser(int pageSize, int page, string name, string role)
        {

            IDictionary<string, object> dataRes = new Dictionary<string, object>();
            ServerResponse<IDictionary<string, object>> res = new ServerResponse<IDictionary<string, object>>();
            var users = this.adminService.getAllUser(pageSize, page, name, role);
            var count = this.adminService.getAllUserCount(name);
            dataRes.Add("users", users);
            dataRes.Add("count", count);
            res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("user/employee")]
        public ObjectResult updateEmployee([FromBody] UpdateEmployeeDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            ValidationResult result = this.updateEmployeeDtoValidator.Validate(body);
            res.mapDetails(result);

            if (!result.IsValid)
            {
                return new BadRequestObjectResult(res.getResponse());
            }
            UpdateEmployeeDto userUpdate = new UpdateEmployeeDto();
            userUpdate.userId = body.userId;
            userUpdate.role = body.role;
            userUpdate.status = body.status;
            userUpdate.salary = body.salary;


            User user = this.userService.getUserById(userUpdate.userId);
            if (user == null)
            {
                res.setErrorMessage("User with given Id not found");
                return new NotFoundObjectResult(res.getResponse());
            }

            if (user.role.ToString().Equals("MANAGER") || user.role.ToString().Equals("OWNER"))
            {
                res.setErrorMessage("You can't update admin status");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 406 };
            }
            if (userUpdate.role == 1 && userUpdate.salary != 0)
            {
                res.setErrorMessage("You can't update customer salary");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 406 };
            }

            bool isUpdate = this.adminService.updateEmployee(userUpdate);
            if (!isUpdate)
            {
                res.setErrorMessage("Update status user fail");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            res.setMessage("Update Status User successfully");
            return new ObjectResult(res.getResponse());

        }

        [HttpGet("user")]
        public ObjectResult getOneUser(string userId)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            User user = this.userService.getUserById(userId);
            if (user == null)
            {
                res.setErrorMessage("User with given Id was not found");
                return new NotFoundObjectResult(res.getResponse());
            }
            user.password = "";
            res.data = user;
            return new ObjectResult(res.getResponse());
        }
    }
}