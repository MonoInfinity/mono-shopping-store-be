using FluentValidation;

namespace store.Src.ProductModule.DTO
{
    public class DeleteImportInfoDto
    {
        public string importInfoId { get; set; }
        public DeleteImportInfoDto(string importInfoId)
        {
            this.importInfoId = importInfoId;
        }
        public DeleteImportInfoDto()
        {

        }
    }

    public class DeleteImportInfoDtoValidator : AbstractValidator<DeleteImportInfoDto>
    {
        public DeleteImportInfoDtoValidator()
        {
            RuleFor(x => x.importInfoId).NotEmpty();
        }
    }
}