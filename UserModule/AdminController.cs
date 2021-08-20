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
        private readonly IAuthService authService;
        private readonly IAdminService adminService;
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        private readonly RegisterUserDtoValidator registerUserDtoValidator;
        private readonly UpdateUserDtoValidator updateUserDtoValidator;
        public AdminController(IUserService userService, IAuthService authService, IAdminService adminService, LoginUserDtoValidator loginUserDtoValidator, RegisterUserDtoValidator registerUserDtoValidator, UpdateUserDtoValidator updateUserDtoValidator)
        {
            // this.loggerr = loggerr;
            this.userService = userService;
            this.updateUserDtoValidator = updateUserDtoValidator;
            this.loginUserDtoValidator = loginUserDtoValidator;
            this.registerUserDtoValidator = registerUserDtoValidator;
            this.authService = authService;
            this.adminService = adminService;
        }


        [HttpGet("user/all")]

        public ObjectResult listAllUser(int pageSize, int page)
        {
            var users = this.adminService.getAllUser(pageSize, page);

            return new ObjectResult(users);
        }
    }
}