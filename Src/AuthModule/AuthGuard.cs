using Microsoft.VisualBasic.CompilerServices;
using System.Collections.Generic;
using System.Net;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using store.Src.Utils.Interface;
using store.Src.Utils;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using store.Src.UserModule.Interface;
using store.Src.Utils.Common;
using store.Src.UserModule.Entity;

namespace store.Src.AuthModule
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

                var objOut = new Object();

                // check user's role
                if (context.ActionArguments.TryGetValue("roles", out objOut))
                {
                    UserRole[] roles = context.ActionArguments["roles"] as UserRole[];
                    if (!roles.Contains(user.role))
                    {
                        res.setErrorMessage("Action is not allow");
                        context.Result = new ObjectResult(res) { StatusCode = 403 };
                        return;
                    }
                }


                // check user status
                if (user.status == UserStatus.DISABLE)
                {
                    res.setErrorMessage("Action is not allow");
                    context.Result = new UnauthorizedObjectResult(res);
                    return;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error);

                //k du do dai 
                // can fix sau
                res.setErrorMessage("Action is not allow");
                context.Result = new UnauthorizedObjectResult(res);
                return;
            }
        }
    }
}