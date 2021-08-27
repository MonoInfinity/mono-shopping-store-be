using FluentValidation;
using store.Src.Utils.Validator;

namespace store.Src.AuthModule.DTO
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
        public LoginUserDto() { }

    }

    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.username).NotEmpty().Length(UserValidator.USERNAME_MIN, UserValidator.USERNAME_MAX).NotNull();
            RuleFor(x => x.password).NotEmpty().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX).NotNull();
        }
    }
}