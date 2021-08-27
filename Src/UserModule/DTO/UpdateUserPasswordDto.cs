using FluentValidation;
using store.Src.Utils.Validator;

namespace store.Src.UserModule.DTO
{
    public class UpdateUserPasswordDto
    {
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string newPassword { get; set; }
        public UpdateUserPasswordDto()
        {

        }
        public UpdateUserPasswordDto(string password, string newPassword, string confirmPassword)
        {
            this.password = password;
            this.newPassword = newPassword;
            this.confirmPassword = confirmPassword;
        }
    }

    public class UpdateUserPasswordDtoValidator : AbstractValidator<UpdateUserPasswordDto>
    {
        public UpdateUserPasswordDtoValidator()
        {
            RuleFor(x => x.password).NotEmpty().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX).NotNull();
            RuleFor(x => x.newPassword).NotEmpty().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX).NotNull();
            RuleFor(x => x.confirmPassword).NotEmpty().Equal(x => x.newPassword);
        }
    }
}