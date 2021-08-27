namespace store.Src.AuthModule.Interface
{
    public interface IAuthService
    {

        public string hashingPassword(string password);

        public bool comparePassword(string inputPassword, string encryptedPassword);
    }
}