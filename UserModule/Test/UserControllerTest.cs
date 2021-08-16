using System.ComponentModel;

using Xunit;
using store.UserModule.DTO;
using store.UserModule.Entity;
using store.Utils.Interface;
using store.Utils;
using store.Utils.Test;
using store.UserModule.Interface;

namespace store.UserModule.Test
{


    public class UserControllerTest
    {

        private readonly UserController userController;


        public UserControllerTest()
        {

            LoginUserDtoValidator loginUserDtoValidator = new LoginUserDtoValidator();
            RegisterUserDtoValidator registerUserDtoValidator = new RegisterUserDtoValidator();
            ConfigTest config = new ConfigTest();
            IDBHelper dbHelper = new DBHelper(config);
            IUserRepository userRepository = new UserRepository(dbHelper);
            IUserService userService = new UserService(userRepository);
            this.userController = new UserController(userService, loginUserDtoValidator, registerUserDtoValidator);
        }

        [Fact]
        public void passLogin()
        {
            LoginUserDto input = new LoginUserDto("hai", "123");
            var res = this.userController.loginUser(input);
            User user = res["data"] as User;

            Assert.Equal("hai", user.username);
            Assert.Equal("123", user.password);
        }

        [Fact]
        public void FailedInputLogin()
        {
            LoginUserDto input = new LoginUserDto("", "123");
            var res = this.userController.loginUser(input);


            Assert.Null(res["data"]);
            Assert.NotNull(res["details"]);
        }

        [Fact]
        public void FailedNotFoundUserLogin()
        {
            LoginUserDto input = new LoginUserDto("12361632", "123");
            var res = this.userController.loginUser(input);


            Assert.Null(res["data"]);
            Assert.NotNull(res["details"]);
        }
    }
}