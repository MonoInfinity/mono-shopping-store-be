using System.IO;
using FluentValidation;
using store.Utils.Validator;
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

    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.username).NotEmpty().Length(3, 10).NotNull();
            RuleFor(x => x.password).NotEmpty().Length(3, 10).NotNull();
        }
    }
}