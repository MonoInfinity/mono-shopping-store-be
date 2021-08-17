using System.IO;
using FluentValidation;
using store.Utils.Validator;

namespace store.UserModule.DTO
{
    public class UpdateUserPasswordDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string newPassword { get; set; }

        public UpdateUserPasswordDto(string username, string password, string newPassword, string confirmPassword)
        {
            this.username = username;
            this.password = password;
            this.newPassword = newPassword;
            this.confirmPassword = confirmPassword;
        }
    }

    public class UpdateUserPasswordDtoValidater : AbstractValidator<UpdateUserPasswordDto>
    {
        public UpdateUserPasswordDtoValidater()
        {
            RuleFor(x => x.username).NotEmpty().Length(UserValidator.USERNAME_MIN, UserValidator.USERNAME_MAX).NotNull();
            RuleFor(x => x.password).NotEmpty().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX).NotNull();
            RuleFor(x => x.newPassword).NotEmpty().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX).NotNull();
            RuleFor(x => x.confirmPassword).NotEmpty().Equal(x => x.newPassword);
        }
    }
}