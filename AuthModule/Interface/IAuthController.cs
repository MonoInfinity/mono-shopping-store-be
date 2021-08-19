using System;
using System.Collections.Generic;
using store.AuthModule.DTO;

using Microsoft.AspNetCore.Mvc;

namespace store.AuthModule.Interface
{
    public interface IAuthController
    {
        public ObjectResult loginUser(LoginUserDto body);
        public ObjectResult registerUser(RegisterUserDto body);
    }
}