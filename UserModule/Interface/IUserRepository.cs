using System.Collections.Generic;
using store.UserModule.Entity;

namespace store.UserModule.Interface
{
    public interface IUserRepository
    {
        public User getUserByUsername(string username);

        public User getUserByUserId(string userId);
        public List<User> getAllUsers(int pageSize, int currentPage);

        public bool saveUser(User user);

        public bool updateUser(User user);
    }
}