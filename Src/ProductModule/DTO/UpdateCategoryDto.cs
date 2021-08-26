using FluentValidation;
using store.Src.ProductModule.Entity;


namespace store.Src.ProductModule.DTO
{
    public class UpdateCategoryDto
    {
        public string name { get; set; }
        public CategoryStatus status { get; set; }
        public string categoryId { get; set; }
        public UpdateCategoryDto()
        {

        }

        public UpdateCategoryDto(string categoryId, string name, CategoryStatus status)
        {
            this.name = name;
            this.status = status;
            this.categoryId = categoryId;
        }

    }
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.categoryId).NotEmpty().NotNull();
            RuleFor(x => x.name).NotEmpty().Length(1, 40).NotNull();
            RuleFor(x => x.status).NotNull();
        }
    }
}