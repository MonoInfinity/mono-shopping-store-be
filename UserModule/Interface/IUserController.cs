using System;
using System.Collections.Generic;
using store.UserModule.DTO;


namespace store.UserModule.Interface
{
    public interface IUserController
    {
        public IDictionary<string, Object> loginUser(LoginUserDto body);

        public IDictionary<string, Object> registerUser(RegisterUserDto body);

        public IDictionary<string, Object> logger();
    }
}
