using FluentValidation;

namespace store.ProductModule.DTO
{
    public class AddCategoryDto
    {
        public string name { get; set; }

        public AddCategoryDto() { }
        public AddCategoryDto(string name) {
            this.name = name;
        }
    }

    public class AddCategoryDtoValidator: AbstractValidator<AddCategoryDto>
    {
        public AddCategoryDtoValidator(){
            RuleFor(x => x.name).NotEmpty().Length(3, 40).NotNull();
        }
    }
}