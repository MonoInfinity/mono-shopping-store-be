
using store.Src.UserModule.DTO;
using Microsoft.AspNetCore.Mvc;

namespace store.Src.UserModule.Interface
{
    public interface IAdminController
    {
        public ObjectResult listAllUser(int pageSize, int page, string name);
        public ObjectResult updateEmployee(UpdateEmployeeDto body);
    }
}