using System.Collections.Generic;

using store.UserModule.Entity;
using store.UserModule.Interface;
using System;
using store.UserModule.DTO;

namespace store.UserModule
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository userRepository;
        public AdminService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public List<User> getAllUser(int currentPage, int pageSize, string name)
        {
            var users = this.userRepository.getAllUsers(pageSize, currentPage, name);

            return users;
        }

        public int getAllUserCount(string name)
        {
            var count = this.userRepository.getAllUsersCount(name);
            return count;
        }

        public bool updateStatusUser(UpdateStatusUserDto updateStatusUserDto)
        {
            bool result = this.userRepository.updateStatusUser(updateStatusUserDto);
            return result;
        }
    }
}