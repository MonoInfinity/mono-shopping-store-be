
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

        public UpdateUserDto(string newName, string newEmail, string newPhone, string newAddress
        )
        {
            this.newName = newName;
            this.newEmail = newEmail;
            this.newPhone = newPhone;
            this.newAddress = newAddress;
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
        }
    }
}