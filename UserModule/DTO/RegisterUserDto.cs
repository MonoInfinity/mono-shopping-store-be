
using FluentValidation;
using store.Utils.Validator;

namespace store.UserModule.DTO
{
    public class RegisterUserDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }

        public RegisterUserDto(string username, string password, string confirmPassword, string name, string email, string phone, string address){
            this.username = username;
            this.password = password;
            this.confirmPassword = confirmPassword;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.address = address;
        }
    }

    public class RegisterUserDtoValidator: AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.username).NotEmpty().Length(UserValidator.USERNAME_MIN, UserValidator.USERNAME_MAX);
            RuleFor(x => x.password).NotEmpty().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX);
            RuleFor(x => x.confirmPassword).NotEmpty().Equal(x => x.password);
            RuleFor(x => x.name).NotEmpty().Length(UserValidator.NAME_MIN, UserValidator.NAME_MAX);
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.phone).NotEmpty();
            RuleFor(x => x.address).NotEmpty().Length(UserValidator.ADDRESS_MIN, UserValidator.ADDRESS_MAX);
        }
    }
}