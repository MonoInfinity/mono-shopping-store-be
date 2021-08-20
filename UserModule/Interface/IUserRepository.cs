using store.UserModule.Entity;

namespace store.UserModule.Interface
{
    public interface IUserRepository
    {
        public User getUserByUsername(string username);

        public User getUserByUserId(string userId);

        public bool saveUser(User user);

        public bool updateUser(User user);

        public bool updateUserPassword(string userId, string password);
    }
}