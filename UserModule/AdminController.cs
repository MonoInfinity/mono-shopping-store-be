using System;
using System.Collections.Generic;
using FluentValidation.Results;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using store.Utils.Interface;
using store.AuthModule.DTO;
using store.AuthModule.Interface;
using store.UserModule.DTO;
using store.UserModule.Entity;
using store.UserModule.Interface;
using store.Utils.Common;
using store.AuthModule;
using System.Diagnostics;



namespace store.UserModule
{

    [ApiController]
    [Route("/api/admin")]
    [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER, UserRole.OWNER })]
    [ServiceFilter(typeof(AuthGuard))]
    public class AdminController : Controller, IAdminController
    {

        private readonly IUserService userService;

        private readonly IAdminService adminService;
        private readonly UpdateStatusUserDtoValidator updateStatusUserDtoValidator;

        public AdminController(IUserService userService, IAdminService adminService, UpdateStatusUserDtoValidator updateStatusUserDtoValidator)
        {

            this.userService = userService;
            this.adminService = adminService;
            this.updateStatusUserDtoValidator = updateStatusUserDtoValidator;

        }


        [HttpGet("user/all")]
        public ObjectResult listAllUser(int pageSize, int page)
        {
            ServerResponse<List<User>> res = new ServerResponse<List<User>>();
            var users = this.adminService.getAllUser(pageSize, page);

            res.data = users;
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("user/status")]
        public ObjectResult updateStatusUser([FromBody] UpdateStatusUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            ValidationResult result = this.updateStatusUserDtoValidator.Validate(body);
            res.mapDetails(result);

            if (!result.IsValid)
            {
                return new BadRequestObjectResult(res.getResponse());
            }
            UpdateStatusUserDto userUpdate = new UpdateStatusUserDto();
            userUpdate.userId = body.userId;
            User user = this.userService.getUserById(userUpdate.userId);

            if (user.role.ToString().Equals("MANAGER") || user.role.ToString().Equals("OWNER"))
            {
                res.setErrorMessage("You can't update admin status");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 406 };
            }
            else
            {
                bool isUpdateStatus = this.adminService.updateStatusUser(userUpdate);
                if (!isUpdateStatus)
                {
                    res.setErrorMessage("Update status user fail");
                    return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
                }
                else
                {
                    res.setMessage("Update Status User successfully");
                    return new ObjectResult(res.getResponse());
                }
            }

        }
    }
}