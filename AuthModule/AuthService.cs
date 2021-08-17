using mono_store_be.AuthModule.Interface;
using mono_store_be.Utils.Interface;

namespace mono_store_be.AuthModule
{
    public class AuthService: IAuthService
    {
        private readonly IJwtService jwtService;

        public AuthService(IJwtService jwtService){
            this.jwtService = jwtService;
        }
    }
}