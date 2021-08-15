

namespace store.UserModule.Entity
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }


        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public override string ToString()
        {
            return "User: " + username + " " + password;
        }
    }
}
