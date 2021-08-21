using System.Collections.Generic;
using store.UserModule.Entity;
using store.UserModule.DTO;

namespace store.UserModule.Interface
{
    public interface IAdminService
    {
        public List<User> getAllUser(int currentPage, int pageSize, string name);
        public int getAllUserCount(string name);
        public bool updateStatusUser(UpdateStatusUserDto updateStatusUserDto);
    }
}