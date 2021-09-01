using FluentValidation;

namespace store.Src.OrderModule.DTO
{
    public class CreateItemDto
    {
        public int quantity { get; set; }
        public string productId { get; set; }


        public CreateItemDto() { }
        public CreateItemDto(int quantity, string productId)
        {
            this.quantity = quantity;
            this.productId = productId;
        }
    }

    public class CreateItemDtoValidator : AbstractValidator<CreateItemDto>
    {
        public CreateItemDtoValidator()
        {
            RuleFor(x => x.quantity).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.productId).NotEmpty().NotNull();
        }
    }
}