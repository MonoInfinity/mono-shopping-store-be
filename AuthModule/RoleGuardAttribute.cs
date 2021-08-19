using Microsoft.AspNetCore.Mvc.Filters;
using store.UserModule.Entity;

namespace store.AuthModule
{
    public class RoleGuardAttribute : ActionFilterAttribute
    {
        private UserRole[] roles;
        public RoleGuardAttribute(UserRole[] roles)
        {
            this.roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["roles"] = this.roles;
        }
    }
}