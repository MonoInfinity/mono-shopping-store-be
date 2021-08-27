using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using store.Src.UserModule.DTO;
using store.Src.UserModule.Entity;
using store.Src.UserModule.Interface;
using store.Src.Utils.Common;
using store.Src.AuthModule;
using static store.Src.Utils.Locale.CustomLanguageValidator;
using store.Src.Utils.Validator;

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
        [ValidateFilterAttribute(typeof(UpdateEmployeeDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult updateEmployee([FromBody] UpdateEmployeeDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();

            UpdateEmployeeDto userUpdate = new UpdateEmployeeDto();
            userUpdate.userId = body.userId;
            userUpdate.role = body.role;
            userUpdate.status = body.status;
            userUpdate.salary = body.salary;


            User user = this.userService.getUserById(userUpdate.userId);
            if (user == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "userId");
                return new NotFoundObjectResult(res.getResponse());
            }

            if (user.role.ToString().Equals("MANAGER") || user.role.ToString().Equals("OWNER"))
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotAllow);
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 403 };
            }
            if (userUpdate.role == 1 && userUpdate.salary != 0)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotAllow);
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 403 };
            }

            bool isUpdate = this.adminService.updateEmployee(userUpdate);
            if (!isUpdate)
            {
                res.setErrorMessage(ErrorMessageKey.Error_FailToSave);
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            res.setMessage(MessageKey.Message_UpdateSuccess);
            return new ObjectResult(res.getResponse());

        }

        [HttpGet("user")]
        public ObjectResult getOneUser(string userId)
        {
            ServerResponse<User> res = new ServerResponse<User>();
            User user = this.userService.getUserById(userId);
            if (user == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "userId");
                return new NotFoundObjectResult(res.getResponse());
            }
            user.password = "";
            res.data = user;
            return new ObjectResult(res.getResponse());
        }
    }
}