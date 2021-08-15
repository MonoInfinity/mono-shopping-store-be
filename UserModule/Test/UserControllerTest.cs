using System.ComponentModel;

using Xunit;
using store.UserModule.DTO;
using store.UserModule.Entity;
using store.Utils;
using store.Utils.Test;


namespace store.UserModule.Test
{


    public class UserControllerTest
    {

        private readonly UserController userController;


        public UserControllerTest()
        {
            ConfigTest config = new ConfigTest();
            IDBHelper dbHelper = new DBHelper(config);
            IUserRepository userRepository = new UserRepository(dbHelper);
            IUserService userService = new UserService(userRepository);
            this.userController = new UserController(userService);
        }

        [Fact]
        public void testLogin()
        {
            LoginUserDto input = new LoginUserDto("hai", "123");
            var tet = this.userController.loginUser(input);

            Assert.Equal(tet, "sdok");
        }
    }
}