using FluentValidation;


namespace store.ProductModule.DTO
{
    public class AddSubCategoryDto
    {
        public string name { get; set; }
        public string categoryId { get; set; }
        public AddSubCategoryDto() { }
        public AddSubCategoryDto(string name, string categoryId) {
            this.name = name;
            this.categoryId = categoryId;
        }
    }

    public class AddSubCategoryDtoValidator:AbstractValidator<AddSubCategoryDto>
    {
        public AddSubCategoryDtoValidator(){
            RuleFor(x => x.name).NotEmpty().Length(1, 40).NotNull();
            RuleFor(x => x.categoryId).NotEmpty().NotNull();
        }
    }
}