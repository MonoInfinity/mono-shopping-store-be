using store.Src.ProductModule.Entity;
using FluentValidation;

namespace store.Src.ProductModule.DTO
{
    public class UpdateSubCategoryDto
    {
        public string name { get; set; }
        public SubCategoryStatus status { get; set; }
        public string subCategoryId { get; set; }
        public UpdateSubCategoryDto()
        {

        }

        public UpdateSubCategoryDto(string name, SubCategoryStatus status, string subCategoryId)
        {
            this.name = name;
            this.status = status;
            this.subCategoryId = subCategoryId;
        }

    }
    public class UpdateSubCategoryDtoValidator : AbstractValidator<UpdateSubCategoryDto>
    {
        public UpdateSubCategoryDtoValidator()
        {
            RuleFor(x => x.subCategoryId).NotEmpty().NotNull();
            RuleFor(x => x.name).NotEmpty().Length(1, 40).NotNull();
            RuleFor(x => x.status).NotNull();
        }
    }
}