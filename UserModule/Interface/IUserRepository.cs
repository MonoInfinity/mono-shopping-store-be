using store.UserModule.Entity;

namespace store.UserModule.Interface
{
    public interface IUserRepository
    {
        public User getUserByUsername(string username);

        public bool saveUser(User user);

        public bool updateUser(User user);
    }
}