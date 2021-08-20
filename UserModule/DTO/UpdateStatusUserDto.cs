using FluentValidation;
using store.Utils.Validator;

namespace store.UserModule.DTO
{
    public class UpdateStatusUserDto
    {
        public string userId { get; set; }
        public UpdateStatusUserDto(string userId)
        {
            this.userId = userId;
        }

        public UpdateStatusUserDto()
        {

        }
    }

    public class UpdateStatusUserDtoValidator : AbstractValidator<UpdateStatusUserDto>
    {
        public UpdateStatusUserDtoValidator()
        {
            RuleFor(x => x.userId).NotEmpty().Length(30, 40);
        }
    }
}