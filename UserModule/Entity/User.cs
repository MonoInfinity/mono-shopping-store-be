

using System;

namespace store.UserModule.Entity
{

    public enum UserRole
    {
        CUSTOMER = 1,
        SHIPPER = 2,
        CASHIER = 3,
        MANAGER = 4,
        OWNER = 5,
    }

    public enum UserStatus
    {
        DISABLE = 0,
        ENABLE = 1
    }
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
        public UserRole role { get; set; }
        public UserStatus status { get; set; }

        public User()
        {
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
            this.role = UserRole.CUSTOMER;
            this.status = UserStatus.ENABLE;
        }

        public override string ToString()
        {
            return "User: \nUserId: " + userId + " \nUsername: " + username + " \nPassword: " + password + " \nName: " +
                            name + " \nEmail: " + email + " \nPhone: " + phone + " \nAddress: " + address + " \nGoogleId: " +
                            googleId + " \nCreateDate: " + createDate + " \nSalary: " + salary + " \nRole: " + role + " \nStatus: " + status;
        }
    }
}
