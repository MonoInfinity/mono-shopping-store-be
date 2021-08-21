using store.Src.UserModule.Entity;
using store.Src.UserModule.Interface;
using System;

namespace store.Src.UserModule
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {

            this.userRepository = userRepository;
        }

        public User getUserById(string id)
        {
            User user = this.userRepository.getUserByUserId(id);
            return user;
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

        public bool updateUser(User user)
        {
            bool result = this.userRepository.updateUser(user);
            return result;
        }
    }
}