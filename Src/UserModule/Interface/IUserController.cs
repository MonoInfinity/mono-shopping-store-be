using store.Src.UserModule.DTO;
using Microsoft.AspNetCore.Mvc;

namespace store.Src.UserModule.Interface
{
    public interface IUserController
    {

        public ObjectResult updateUser(UpdateUserDto body);
        public ObjectResult getUser();
        public ObjectResult updateUserPassword(UpdateUserPasswordDto body);
    }
}
