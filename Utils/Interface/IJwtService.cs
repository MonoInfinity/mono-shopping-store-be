using store.UserModule.Entity;

namespace mono_store_be.Utils.Interface
{
    public interface IJwtService
    {
        public string GenerateToken(string data);

        public string VerifyToken(string token);
    }
}