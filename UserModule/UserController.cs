

using System;
using System.Collections.Generic;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using store.UserModule.DTO;
using store.UserModule.Entity;
using store.Utils.Common;
using store.Utils;
namespace store.UserModule
{
    [ApiController]
    [Route("/api/user")]
    public class UserController
    {

        private readonly IUserService userService;
        private readonly IRedis redis;
        private readonly LoginUserDtoValidator loginUserDtoValidator;
        public UserController(IUserService userService, LoginUserDtoValidator loginUserDtoValidator, IRedis redis)
        {
            this.redis = redis;
            this.userService = userService;
            this.loginUserDtoValidator = loginUserDtoValidator;
        }

        [HttpPost("login")]
        public IDictionary<string, Object> loginUser([FromBody] LoginUserDto body)
        {
            ServerResponse<User> res = new ServerResponse<User>();

            ValidationResult result = this.loginUserDtoValidator.Validate(body);
            res.mapDetails(result);

            if (!result.IsValid)
            {
                return res.getResponse();
            }



            User user = userService.getUserByUsername(body.username);
            if (user == null)
            {
                res.setErrorMessage("User with the given id was not found");
                return res.getResponse();
            }


            res.data = user;
            return res.getResponse();
        }
    }
}