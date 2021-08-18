
using FluentValidation;
using store.Utils.Validator;
namespace store.UserModule.DTO
{
    public class UpdateUserDto
    {
        public string username { get; set; }
        public string newName { get; set; }
        public string newEmail { get; set; }
        public string newPhone { get; set; }
        public string newAddress { get; set; }

        public UpdateUserDto(string username, string newName, string newEmail, string newPhone, string newAddress
        )
        {
            this.username = username;
            this.newName = newName;
            this.newEmail = newEmail;
            this.newPhone = newPhone;
            this.newAddress = newAddress;
        }
    }
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.username).NotEmpty().Length(3, 20).NotNull();
            RuleFor(x => x.newName).NotEmpty().Length(3, 20).NotNull();
            RuleFor(x => x.newEmail).NotEmpty().Length(3, 30).NotNull();
            RuleFor(x => x.newPhone).NotEmpty().Length(7, 11).NotNull();
            RuleFor(x => x.newAddress).NotEmpty().Length(3, 20).NotNull();
        }
    }
}