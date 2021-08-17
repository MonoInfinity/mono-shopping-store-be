using store.UserModule.Entity;
using store.UserModule.Interface;

namespace store.UserModule
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {

            this.userRepository = userRepository;
        }

        public User getUserByUsername(string username)
        {
            User user = this.userRepository.getUserByUsername(username);
            return user;
        }

        public bool saveUser(User user)
        {
            bool res = this.userRepository.saveUser(user);
            return res;
        }

        public string hashingPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 8);
        }

        public bool comparePassword(string inputPassword, string encryptedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, encryptedPassword);
        }
    }
}