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
using store.ProductModule.DTO;

namespace store.Utils.Validator
{
    public class ValidateFilter : IActionFilter
    {
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        private readonly RegisterUserDtoValidator registerUserDtoValidator;
        private readonly UpdateUserDtoValidator updateUserDtoValidator;
        private readonly AddCategoryDtoValidator addCategoryDtoValidator;
        private readonly AddSubCategoryDtoValidator addSubCategoryDtoValidator;
        private readonly AddProductDtoValidator addProductDtoValidator;
        private readonly UpdateProductDtoValidator updateProductDtoValidator;

        public ValidateFilter(
                                LoginUserDtoValidator loginUserDtoValidator,
                                RegisterUserDtoValidator registerUserDtoValidator,
                                UpdateUserDtoValidator updateUserDtoValidator,
                                AddCategoryDtoValidator addCategoryDtoValidator,
                                AddSubCategoryDtoValidator addSubCategoryDtoValidator,
                                AddProductDtoValidator addProductDtoValidator,
                                UpdateProductDtoValidator updateProductDtoValidator
                            )
        {
            this.loginUserDtoValidator = loginUserDtoValidator;
            this.registerUserDtoValidator = registerUserDtoValidator;
            this.updateUserDtoValidator = updateUserDtoValidator;
            this.addCategoryDtoValidator = addCategoryDtoValidator;
            this.addSubCategoryDtoValidator = addSubCategoryDtoValidator;
            this.addProductDtoValidator = addProductDtoValidator;
            this.updateProductDtoValidator = updateProductDtoValidator;
        }

        private T assignValue<T>(string bodyString, Type type)
        {
            bodyString = bodyString.Replace("}", "");
            bodyString = bodyString.Replace("{", "");
            bodyString = bodyString.Replace(Environment.NewLine, "");
            bodyString = bodyString.Trim();
            bodyString = bodyString.Replace(",", ":");

            string[] pairs = bodyString.Split(":");
            for (int i = 0; i < pairs.Length; i++) pairs[i] = pairs[i].Trim();
            // after all of the above code, we have something like ["username","haicao","password","123","age",20]

            // them we assign the value of pairs into obj
            var obj = (T)Activator.CreateInstance(type);
            PropertyInfo[] propertys = obj.GetType().GetProperties();
            for (int i = 0; i < propertys.Length; i++)
            {
                string propertyValue = findValueOfProperty(propertys[i].Name, pairs);

                if (propertyValue != "")
                {
                    // value is a string
                    if (propertyValue.Contains("\""))
                    {
                        propertyValue = propertyValue.Replace("\"", "");
                        propertys[i].SetValue(obj, propertyValue);
                    }

                    else
                    {
                        // value is a double
                        if (propertys[i].PropertyType == typeof(Double))
                        {
                            propertys[i].SetValue(obj, Double.Parse(propertyValue));
                        }
                        // value is a int32
                        if (propertys[i].PropertyType == typeof(Int32))
                        {
                            propertys[i].SetValue(obj, Int32.Parse(propertyValue));
                        }
                    }
                }
            }

            return obj;
        }

        private string findValueOfProperty(string propertyName, string[] strArray)
        {
            for (int i = 0; i < strArray.Length; i = i + 2)
            {
                if (strArray[i].Equals("\"" + propertyName + "\""))
                {
                    return strArray[i + 1];
                }
            }
            return "";
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

            // AuthModule DTO
            if (typeof(LoginUserDto) == dtoType)
            {
                result = this.loginUserDtoValidator.Validate(assignValue<LoginUserDto>(bodyStr, dtoType));
            }
            if (typeof(RegisterUserDto) == dtoType)
            {
                result = this.registerUserDtoValidator.Validate(assignValue<RegisterUserDto>(bodyStr, dtoType));
            }

            // UserModule DTO
            if (typeof(UpdateUserDto) == dtoType)
            {
                result = this.updateUserDtoValidator.Validate(assignValue<UpdateUserDto>(bodyStr, dtoType));
            }


            // ProductModule DTO
            if (typeof(AddCategoryDto) == dtoType)
            {
                result = this.addCategoryDtoValidator.Validate(assignValue<AddCategoryDto>(bodyStr, dtoType));
            }
            if (typeof(AddSubCategoryDto) == dtoType)
            {
                result = this.addSubCategoryDtoValidator.Validate(assignValue<AddSubCategoryDto>(bodyStr, dtoType));
            }
            if (typeof(AddProductDto) == dtoType)
            {
                result = this.addProductDtoValidator.Validate(assignValue<AddProductDto>(bodyStr, dtoType));
            }
            if (typeof(UpdateProductDto) == dtoType)
            {
                result = this.updateProductDtoValidator.Validate(assignValue<UpdateProductDto>(bodyStr, dtoType));
            }


            if (!result.IsValid)
            {
                res.mapDetails(result);
                context.Result = new BadRequestObjectResult(res.getResponse());
                return;
            }
        }
    }
}