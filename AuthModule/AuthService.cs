using store.AuthModule.Interface;
using store.Utils.Interface;


namespace store.AuthModule
{
    public class AuthService : IAuthService
    {

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