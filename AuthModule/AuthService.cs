using mono_store_be.AuthModule.Interface;
using mono_store_be.Utils.Interface;
using store.AuthModule.Interface;

namespace mono_store_be.AuthModule
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService jwtService;

        public AuthService(IJwtService jwtService)
        {
            this.jwtService = jwtService;
        }

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