using FluentValidation;

namespace store.Src.OrderModule.DTO
{
    public class CreateItemDto
    {
        public int quantity { get; set; }
        public double salePrice { get; set; }
        public string productId { get; set; }
        public string orderId { get; set; }


        public CreateItemDto() { }
        public CreateItemDto(int quantity, double salePrice, string productId, string orderId)
        {
            this.quantity = quantity;
            this.salePrice = salePrice;
            this.productId = productId;
            this.orderId = orderId;
        }
    }

    public class CreateItemDtoValidator : AbstractValidator<CreateItemDto>
    {
        public CreateItemDtoValidator()
        {
            RuleFor(x => x.quantity).NotEmpty().NotNull().GreaterThan(1);
            RuleFor(x => x.salePrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.productId).NotEmpty().NotNull();
            RuleFor(x => x.orderId).NotEmpty().NotNull();
        }
    }
}