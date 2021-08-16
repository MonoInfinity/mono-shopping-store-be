using store.UserModule.Entity;

namespace store.UserModule.Interface
{
    public interface IUserService
    {
        public User getUserByUsername(string username);

        public bool saveUser(User user);
    }
}