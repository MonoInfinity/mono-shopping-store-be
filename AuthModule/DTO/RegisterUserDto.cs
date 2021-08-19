
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using FluentValidation;
using store.Utils.Validator;

namespace store.AuthModule.DTO
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
        public RegisterUserDto()
        {

        }
        public RegisterUserDto(string username, string password, string confirmPassword, string name, string email, string phone, string address)
        {
            this.username = username;
            this.password = password;
            this.confirmPassword = confirmPassword;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.address = address;
        }
    }

    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.username).NotEmpty().Length(UserValidator.USERNAME_MIN, UserValidator.USERNAME_MAX);
            RuleFor(x => x.password).NotEmpty().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX);
            RuleFor(x => x.confirmPassword).NotEmpty().Equal(x => x.password);
            RuleFor(x => x.name).NotEmpty().Length(UserValidator.NAME_MIN, UserValidator.NAME_MAX);
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.phone).NotEmpty().Custom((value, context)=>{
                Regex defaultFormat = new Regex(@"^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b");
                if(!defaultFormat.IsMatch(value)){
                    context.AddFailure("Invalid phone number");
                }
                else return;
            });
            RuleFor(x => x.address).NotEmpty().Length(UserValidator.ADDRESS_MIN, UserValidator.ADDRESS_MAX);
        }
    }
}