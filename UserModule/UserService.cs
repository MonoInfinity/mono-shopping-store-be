using store.UserModule.Entity;

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
    }
}