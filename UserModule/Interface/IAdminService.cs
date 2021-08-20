using System.Collections.Generic;
using store.UserModule.Entity;

namespace store.UserModule.Interface
{
    public interface IAdminService
    {
        public List<User> getAllUser(int currentPage, int pageSize);
    }
}