// using System;

// using store.Src.UserModule.DTO;
// using store.Src.UserModule.Entity;
// using store.Src.UserModule.Interface;
// using Xunit;
// using store.Src.UserModule;

// using store.Src.Utils.Interface;
// using store.Src.Utils;
// using store.Src.Utils.Test;
// using store.Src.AuthModule.Interface;
// using store.Src.AuthModule.DTO;
// namespace store.Src.AuthModule.Test
// {
//     public class AuthControllerTest
//     {
//         private readonly IAuthController authController;
//         private readonly IUserRepository userRepository;
//         private readonly IUserService userService;
//         private readonly IAuthService authService;
//         private readonly User user;



//         public AuthControllerTest()
//         {

//             UpdateUserDtoValidator updateUserDtoValidator = new UpdateUserDtoValidator();
//             ConfigTest config = new ConfigTest();
//             IDBHelper dbHelper = new DBHelper(config);
//             IJwtService jwtService = new JwtService(config);
//             this.userRepository = new UserRepository(dbHelper);
//             this.userService = new UserService(userRepository);
//             this.authService = new AuthService();
//             this.authController = new AuthController(userService, authService, jwtService);

//             this.user = new User();
//             this.user.userId = Guid.NewGuid().ToString();
//             this.user.username = TestHelper.randomString(10, RamdomStringType.LETTER_LOWER_CASE);
//             this.user.password = this.authService.hashingPassword("123456789");

//             this.userRepository.saveUser(user);
//         }

//         [Fact]
//         public void passLogin()
//         {
//             LoginUserDto input = new LoginUserDto(this.user.username, "123456789");
//             var res = this.authController.loginUser(input);

//             Assert.NotNull(res.Value);
//         }

//         [Fact]
//         public void FailedInputLogin()
//         {
//             LoginUserDto input = new LoginUserDto("", "123");
//             var res = this.authController.loginUser(input);



//             Assert.Equal(400, res.StatusCode);
//         }

//         [Fact]
//         public void FailedNotFoundUserLogin()
//         {
//             LoginUserDto input = new LoginUserDto("12361632", "123");
//             var res = this.authController.loginUser(input);

//             Assert.Equal(400, res.StatusCode);
//         }


//         [Fact]
//         public void PassRegister()
//         {

//             RegisterUserDto input = new RegisterUserDto()
//             {
//                 username = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
//                 password = "123456789",
//                 confirmPassword = "123456789",
//                 name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
//                 email = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE) + "@gmail.com",
//                 phone = "0901212345",
//                 address = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
//             };
//             var res = this.authController.registerUser(input);
//             var user = this.userRepository.getUserByUsername(input.username);

//             Assert.NotNull(user);
//             Assert.Equal(user.username, input.username);
//             Assert.Null(res.StatusCode);
//         }

//         [Fact]
//         public void FailedRegisterUsernameTaken()
//         {

//             RegisterUserDto input = new RegisterUserDto()
//             {
//                 username = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
//                 password = "123456789",
//                 confirmPassword = "123456789",
//                 name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
//                 email = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE) + "@gmail.com",
//                 phone = "0901212345",
//                 address = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
//             };
//             this.authController.registerUser(input);
//             var res = this.authController.registerUser(input);

//             Assert.Equal(400, res.StatusCode);
//         }

//     }
// }