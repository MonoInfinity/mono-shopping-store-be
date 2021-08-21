using store.Src.UserModule.Entity;

namespace store.Src.UserModule.Interface
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