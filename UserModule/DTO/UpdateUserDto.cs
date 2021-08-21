
using FluentValidation;
using store.Utils.Validator;
namespace store.UserModule.DTO
{
    public class UpdateUserDto
    {
        public string newName { get; set; }
        public string newEmail { get; set; }
        public string newPhone { get; set; }
        public string newAddress { get; set; }
        public string newAvatarUrl { get; set; }

        public UpdateUserDto(string newName, string newEmail, string newPhone, string newAddress, string newAvatarUrl
        )
        {
            this.newName = newName;
            this.newEmail = newEmail;
            this.newPhone = newPhone;
            this.newAddress = newAddress;
            this.newAvatarUrl = newAvatarUrl;
        }

        public UpdateUserDto() { }
    }
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.newName).NotEmpty().Length(UserValidator.NAME_MIN, UserValidator.NAME_MAX);
            RuleFor(x => x.newEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.newPhone).NotEmpty().Length(7, 11).NotNull();
            RuleFor(x => x.newAddress).NotEmpty().Length(UserValidator.ADDRESS_MIN, UserValidator.ADDRESS_MAX);
            RuleFor(x => x.newAvatarUrl).NotEmpty().Length(1, 50).NotNull();
        }
    }
}