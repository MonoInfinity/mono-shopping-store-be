
using System;
using Microsoft.AspNetCore.Mvc;
using store.UserModule.DTO;
using store.UserModule.Entity;

namespace store.UserModule
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : IUserController
    {

        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpPost]
        public string loginUser([FromBody] LoginUserDto body)
        {
            User user = userService.getUserByUsername(body.username);
            Console.WriteLine(user);
            return "sdok";
        }
    }
}