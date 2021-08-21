
using store.UserModule.DTO;
using Microsoft.AspNetCore.Mvc;

namespace store.UserModule.Interface
{
    public interface IAdminController
    {
        public ObjectResult listAllUser(int pageSize, int page, string name);
        public ObjectResult updateStatusUser(UpdateStatusUserDto body);

    }
}