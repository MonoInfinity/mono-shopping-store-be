using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace store.Src.Utils.Validator
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        private Type dtoType;
        public ValidateFilterAttribute(Type dtoType)
        {
            this.dtoType = dtoType;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["dtoType"] = this.dtoType;
        }
    }
}