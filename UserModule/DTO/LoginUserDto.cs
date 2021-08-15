namespace store.UserModule.DTO
{
    public class LoginUserDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public LoginUserDto(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

    }
}