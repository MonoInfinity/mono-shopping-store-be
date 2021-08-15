namespace store.UserModule.Entity
{
    public interface IUserRepository
    {
        public User getUserByUsername(string username);
    }
}