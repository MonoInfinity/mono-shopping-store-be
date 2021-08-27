using store.Src.AuthModule.DTO;
using Microsoft.AspNetCore.Mvc;

namespace store.Src.AuthModule.Interface
{
    public interface IAuthController
    {
        public ObjectResult loginUser(LoginUserDto body);
        public ObjectResult registerUser(RegisterUserDto body);
        public ObjectResult logoutUser();
    }
}