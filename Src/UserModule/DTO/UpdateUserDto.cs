
using FluentValidation;
using Microsoft.AspNetCore.Http;
using store.Src.Utils.Validator;
namespace store.Src.UserModule.DTO
{
    public class UpdateUserDto
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string avatarUrl { get; set; }


        public UpdateUserDto(string name, string email, string phone, string address, string avatarUrl
        )
        {
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.address = address;
            this.avatarUrl = avatarUrl;
        }

        public UpdateUserDto() { }
    }
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.name).NotEmpty().Length(UserValidator.NAME_MIN, UserValidator.NAME_MAX).NotNull();
            RuleFor(x => x.email).NotEmpty().EmailAddress().NotNull();
            RuleFor(x => x.phone).NotEmpty().Length(7, 11).NotNull().NotNull();
            RuleFor(x => x.address).NotEmpty().Length(UserValidator.ADDRESS_MIN, UserValidator.ADDRESS_MAX).NotNull();
            RuleFor(x => x.avatarUrl).NotNull();
        }
    }
}