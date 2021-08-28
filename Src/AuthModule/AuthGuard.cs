


using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using store.Src.Utils.Interface;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using store.Src.UserModule.Interface;
using store.Src.Utils.Common;
using store.Src.UserModule.Entity;
using static store.Src.Utils.Locale.CustomLanguageValidator;

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
        string getCookieValueFromContext(ActionExecutingContext context, string cookieName)
        {
            foreach (var headers in context.HttpContext.Request.Headers.Values)
                foreach (var header in headers)
                {
                    if (header.StartsWith($"{cookieName}="))
                    {
                        var p1 = header.IndexOf('=');
                        return header.Substring(p1 + 1, header.Length - p1 - 1);
                    }
                }
            return null;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {

            var res = new ServerResponse<object>();
            var authToken = getCookieValueFromContext(context, "auth-token");
            if (authToken == null)
            {


                res.setErrorMessage(ErrorMessageKey.Error_NotAllow);
                context.Result = new UnauthorizedObjectResult(res.getResponse());
                return;
            }
            try
            {
                var token = this.jwtService.VerifyToken(authToken).Split(";");

                if (token[0] == null)
                {
                    res.setErrorMessage(ErrorMessageKey.Error_NotAllow);
                    context.Result = new UnauthorizedObjectResult(res.getResponse());
                    return;

                }
                var user = this.userService.getUserById(token[0]);
                if (user == null)
                {
                    res.setErrorMessage(ErrorMessageKey.Error_NotAllow);
                    context.Result = new UnauthorizedObjectResult(res.getResponse());
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
                        res.setErrorMessage(ErrorMessageKey.Error_NotAllow);
                        context.Result = new UnauthorizedObjectResult(res.getResponse());
                        return;
                    }
                }


                // check user status
                if (user.status == UserStatus.DISABLE)
                {
                    res.setErrorMessage(ErrorMessageKey.Error_NotAllow);
                    context.Result = new UnauthorizedObjectResult(res.getResponse());
                    return;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error);

                //k du do dai 
                // can fix sau
                res.setErrorMessage(ErrorMessageKey.Error_NotAllow);
                context.Result = new UnauthorizedObjectResult(res.getResponse());
                return;
            }
        }
    }
}