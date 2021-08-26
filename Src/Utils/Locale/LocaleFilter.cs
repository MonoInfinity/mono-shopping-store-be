using System;
using System.Collections.Generic;
using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace store.Src.Utils.Locale
{
    public class LocaleFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var lang = "en";
            var cookies = new Dictionary<string, string>();
            var values = ((string)context.HttpContext.Request.Headers["Cookie"])?.Split(',');
            
            if(values != null){
                foreach (var parts in values)
                {
                    var cookieArray = parts.Trim().Split('=');
                    cookies.Add(cookieArray[0], cookieArray[1]);
                }
                var outValue = "";
                if (cookies.TryGetValue("lang", out outValue))
                {
                    lang = outValue;
                }
            }
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo(lang);
        }
    }
}