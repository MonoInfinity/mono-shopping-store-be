using System.Collections.Generic;

using store.UserModule.Entity;
using store.UserModule.Interface;
using System;

namespace store.UserModule
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository userRepository;
        public AdminService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public List<User> getAllUser(int currentPage, int pageSize)
        {
            var users = this.userRepository.getAllUsers(pageSize, currentPage);
            return users;
        }
    }
}