using store.UserModule.DTO;

namespace store.UserModule
{
    public interface IUserController
    {
        public string loginUser(LoginUserDto body);

    }
}
