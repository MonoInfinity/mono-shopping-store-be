using System;
using System.Collections.Generic;
using store.UserModule.DTO;
using Microsoft.AspNetCore.Mvc;

namespace store.UserModule.Interface
{
    public interface IUserController
    {

        public ObjectResult updateUser(UpdateUserDto body);
        public ObjectResult getUser();
        public ObjectResult updateUserPassword(UpdateUserPasswordDto body);
    }
}
