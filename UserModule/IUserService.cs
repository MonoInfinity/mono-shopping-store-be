using store.UserModule.Entity;

namespace store.UserModule
{
    public interface IUserService
    {
        public User getUserByUsername(string username);
    }
}