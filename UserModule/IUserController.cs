using System;
using System.Collections.Generic;
using store.UserModule.DTO;


namespace store.UserModule
{
    public interface IUserController
    {
        public IDictionary<string, Object> loginUser(LoginUserDto body);
    }
}
