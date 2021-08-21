
using FluentValidation;
using store.Utils.Validator;
namespace store.UserModule.DTO
{
    public class UpdateUserDto
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }


        public UpdateUserDto(string name, string email, string phone, string address
        )
        {
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.address = address;

        }

        public UpdateUserDto() { }
    }
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.name).NotEmpty().Length(UserValidator.NAME_MIN, UserValidator.NAME_MAX);
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.phone).NotEmpty().Length(7, 11).NotNull();
            RuleFor(x => x.address).NotEmpty().Length(UserValidator.ADDRESS_MIN, UserValidator.ADDRESS_MAX);

        }
    }
}