using FluentValidation;

namespace store.Src.OrderModule.DTO
{
    public class AddItemDto
    {
        public string itemId { get; set; }
        public int quantity { get; set; }
        public double salePrice { get; set; }
        public string createDate { get; set; }
        public string productId { get; set; }
        public string orderId { get; set; }


        public AddItemDto() { }
        public AddItemDto(string itemId, int quantity, double salePrice, string createDate, string productId, string orderId)
        {
            this.itemId = itemId;
            this.quantity = quantity;
            this.salePrice = salePrice;
            this.createDate = createDate;
            this.productId = productId;
            this.orderId = orderId;
        }
    }

    public class AddCategoryDtoValidator : AbstractValidator<AddItemDto>
    {
        public AddCategoryDtoValidator()
        {
            RuleFor(x => x.quantity).NotEmpty().NotNull().GreaterThan(1);
            RuleFor(x => x.salePrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.productId).NotEmpty().NotNull();
            RuleFor(x => x.orderId).NotEmpty().NotNull();
        }
    }
}