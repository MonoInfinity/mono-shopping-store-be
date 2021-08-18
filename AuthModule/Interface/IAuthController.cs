using System;
using System.Collections.Generic;
using store.AuthModule.DTO;


namespace store.AuthModule.Interface
{
    public interface IAuthController
    {
        public IDictionary<string, Object> loginUser(LoginUserDto body);

        public IDictionary<string, Object> registerUser(RegisterUserDto body);
    }
}