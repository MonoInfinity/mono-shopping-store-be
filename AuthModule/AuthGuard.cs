using Microsoft.VisualBasic.CompilerServices;
using System.Collections.Generic;
using System.Net;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using store.Utils.Interface;
using store.Utils;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using store.UserModule.Interface;
using store.Utils.Common;
using store.UserModule.Entity;

namespace store.AuthModule
{
    public class AuthGuard : IActionFilter
    {

        private readonly IJwtService jwtService;
        private readonly IUserService userService;
        public AuthGuard(IJwtService jwtService, IUserService userService)
        {
            this.jwtService = jwtService;
            this.userService = userService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {


        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var res = new ServerResponse<object>();

            var cookies = new Dictionary<string, string>();
            var values = ((string)context.HttpContext.Request.Headers["Cookie"]).TrimEnd(';').Split(';');
            foreach (var parts in values)
            {
                var cookieArray = parts.Trim().Split('=');
                cookies.Add(cookieArray[0], cookieArray[1]);
            }
            var outValue = "";
            if (!cookies.TryGetValue("auth-token", out outValue))
            {
                res.setErrorMessage("Action is not allow");
                context.Result = new UnauthorizedObjectResult(res);
                return;
            }

            try
            {
                var token = this.jwtService.VerifyToken(cookies["auth-token"]).Split(";");

                if (token[0] == null)
                {
                    res.setErrorMessage("Action is not allow");
                    context.Result = new UnauthorizedObjectResult(res);
                    return;

                }
                var user = this.userService.getUserById(token[0]);
                if (user == null)
                {
                    res.setErrorMessage("Action is not allow");
                    context.Result = new UnauthorizedObjectResult(res);
                    return;
                }
                Controller controller = context.Controller as Controller;
                controller.ViewData["user"] = user;

                // check user's role
                UserRole[] roles = context.ActionArguments["roles"] as UserRole[];
                if(!roles.Contains(user.role)){
                    res.setErrorMessage("Action is not allow");
                    context.Result = new UnauthorizedObjectResult(res);
                    return;
                }

                // check user status
                if(user.status == UserStatus.DISABLE){
                    res.setErrorMessage("Action is not allow");
                    context.Result = new UnauthorizedObjectResult(res);
                    return;
                }
            }
            catch
            {

                //k du do dai 
                // can fix sau
                res.setErrorMessage("Action is not allow");
                context.Result = new UnauthorizedObjectResult(res);
                return;
            }

        }
    }
}