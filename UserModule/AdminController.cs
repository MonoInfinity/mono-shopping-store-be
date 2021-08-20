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

        public AdminController(IUserService userService, IAdminService adminService)
        {

            this.userService = userService;
            this.adminService = adminService;
        }


        [HttpGet("user/all")]
        public ObjectResult listAllUser(int pageSize, int page)
        {
            ServerResponse<List<User>> res = new ServerResponse<List<User>>();
            var users = this.adminService.getAllUser(pageSize, page);

            res.data = users;
            return new ObjectResult(res.getResponse());
        }
    }
}