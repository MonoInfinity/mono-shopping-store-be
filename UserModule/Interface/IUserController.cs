using System;
using System.Collections.Generic;
using store.UserModule.DTO;


namespace store.UserModule.Interface
{
    public interface IUserController
    {

        public IDictionary<string, Object> updateUser(UpdateUserDto body);

    }
}
