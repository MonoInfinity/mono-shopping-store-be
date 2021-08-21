using FluentValidation;

namespace store.Src.ProductModule.DTO
{
    public class DeleteProductDto
    {
        public string productId { get; set; }
        public DeleteProductDto(string productId)
        {
            this.productId = productId;
        }
        public DeleteProductDto()
        {

        }
    }

    public class DeleteProductDtoValidator : AbstractValidator<DeleteProductDto>
    {
        public DeleteProductDtoValidator()
        {
            RuleFor(x => x.productId).NotEmpty();
        }
    }
}