using System.Collections.Generic;
using store.Src.UserModule.Entity;
using store.Src.UserModule.DTO;

namespace store.Src.UserModule.Interface
{
    public interface IUserRepository
    {
        public User getUserByUsername(string username);

        public User getUserByUserId(string userId);
        public List<User> getAllUsers(int pageSize, int currentPage, string name);
        public int getAllUsersCount(string name);

        public bool saveUser(User user);

        public bool updateUser(User user);
        public bool updateStatusUser(UpdateStatusUserDto updateStatusUserDto);
    }
}