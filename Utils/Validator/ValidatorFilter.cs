using System;
using System.IO;
using System.Reflection;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using store.AuthModule.DTO;
using store.UserModule.Entity;
using store.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using store.UserModule.DTO;

namespace store.Utils.Validator
{
    public class ValidateFilter : IActionFilter
    {
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        private readonly RegisterUserDtoValidator registerUserDtoValidator;
        private readonly UpdateUserDtoValidator updateUserDtoValidator;

        public ValidateFilter(LoginUserDtoValidator loginUserDtoValidator, RegisterUserDtoValidator registerUserDtoValidator, UpdateUserDtoValidator updateUserDtoValidator)
        {
            this.loginUserDtoValidator = loginUserDtoValidator;
            this.registerUserDtoValidator = registerUserDtoValidator;
            this.updateUserDtoValidator = updateUserDtoValidator;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            string bodyStr = string.Empty;
            try
            {
                context.HttpContext.Request.EnableBuffering();
                context.HttpContext.Request.Body.Position = 0;

                using (var reader = new StreamReader(context.HttpContext.Request.Body))
                {
                    bodyStr = await reader.ReadToEndAsync();

                    context.HttpContext.Request.Body.Position = 0;
                }
            }
            catch (Exception) { }

            Type dtoType = (Type)context.ActionArguments["dtoType"];
            ServerResponse<User> res = new ServerResponse<User>();
            ValidationResult result = null;


            if (typeof(LoginUserDto) == dtoType)
            {
                result = this.loginUserDtoValidator.Validate(assignValue<LoginUserDto>(bodyStr, dtoType));
            }
            else if (typeof(RegisterUserDto) == dtoType)
            {
                result = this.registerUserDtoValidator.Validate(assignValue<RegisterUserDto>(bodyStr, dtoType));
            }
            else if(typeof(UpdateUserDto) == dtoType)
            {
                result = this.updateUserDtoValidator.Validate(assignValue<UpdateUserDto>(bodyStr, dtoType));
            }


            if (!result.IsValid)
            {
                res.mapDetails(result);
                context.Result = new BadRequestObjectResult(res.getResponse());
                return;
            }
        }

        private T assignValue<T>(string bodyString, Type type)
        {
            bodyString = bodyString.Replace("}", "");
            bodyString = bodyString.Replace("{", "");
            bodyString = bodyString.Replace(" ", "");
            bodyString = bodyString.Replace("\"", "");
            bodyString = bodyString.Replace(Environment.NewLine, "");
            bodyString = bodyString.Trim();
            bodyString = bodyString.Replace(",", ":");

            string[] pairs = bodyString.Split(":");

            var obj = (T)Activator.CreateInstance(type);
            PropertyInfo[] propertys = obj.GetType().GetProperties();
            for (int i = 0; i < propertys.Length; i++)
            {
                string propertyValue = findValueOfProperty(propertys[i].Name, pairs);
                if (propertyValue != "")
                    propertys[i].SetValue(obj, propertyValue);
            }

            return obj;
        }

        private string findValueOfProperty(string propertyName, string[] strArray)
        {
            for (int i = 0; i < strArray.Length; i = i + 2)
            {
                if (propertyName.Equals(strArray[i]))
                {
                    return strArray[i + 1];
                }
            }
            return "";
        }
    }
}