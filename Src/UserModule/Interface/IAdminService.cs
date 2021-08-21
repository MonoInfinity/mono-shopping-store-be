using System.Collections.Generic;
using store.Src.UserModule.Entity;
using store.Src.UserModule.DTO;

namespace store.Src.UserModule.Interface
{
    public interface IAdminService
    {
        public List<User> getAllUser(int currentPage, int pageSize, string name);
        public int getAllUserCount(string name);
        public bool updateStatusUser(UpdateStatusUserDto updateStatusUserDto);
    }
}