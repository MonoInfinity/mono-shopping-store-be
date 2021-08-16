using store.UserModule.Entity;

namespace store.UserModule.Interface
{
    public interface IUserService
    {
        public User getUserByUsername(string username);

        public bool saveUser(User user);

        public string hashingPassword(string password);

        public bool comparePassword(string inputPassword, string encryptedPasswrod);
    }
}