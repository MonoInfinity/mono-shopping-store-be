

using System;

namespace store.UserModule.Entity
{
    public class User
    {
        public string userId { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string googleId { get; set; }
        public DateTime createDate { get; set; }
        public double salary { get; set; }
        public string role { get; set; }

        public User(){
            this.userId = "";
            this.name = "";
            this.username = "";
            this.password = "";
            this.email = "";
            this.phone = "";
            this.address = "";
            this.googleId = "";
            this.createDate = DateTime.Now;
            this.salary = 0;
            this.role = "";
        }

        public override string ToString()
        {
            return "User: \n" + userId + " \n" + username + " \n" + password + " \n" + name + " \n" + email + " \n" + phone + " \n" + address + " \n" + googleId + " \n" + createDate + " \n" + salary + " \n" + role;
        }
    }
}
