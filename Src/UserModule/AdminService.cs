using System.Collections.Generic;

using store.Src.UserModule.Entity;
using store.Src.UserModule.Interface;
using System;
using store.Src.UserModule.DTO;

namespace store.Src.UserModule
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