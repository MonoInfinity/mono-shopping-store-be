using System.Collections.Generic;
using store.UserModule.Entity;
using store.UserModule.DTO;

namespace store.UserModule.Interface
{
    public interface IAdminService
    {
        public List<User> getAllUser(int currentPage, int pageSize);
        public bool updateStatusUser(UpdateStatusUserDto updateStatusUserDto);
    }
}