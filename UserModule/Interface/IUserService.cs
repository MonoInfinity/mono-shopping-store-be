using store.UserModule.Entity;

namespace store.UserModule.Interface
{
    public interface IUserService
    {
        public User getUserByUsername(string username);
        public User getUserById(string id);

        public bool saveUser(User user);

        public bool updateUser(User user);

        public bool updateUserPassword(string userId, string password);
    }
}