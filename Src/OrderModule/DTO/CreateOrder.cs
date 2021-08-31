using FluentValidation;

namespace store.Src.OrderModule.DTO
{
    public class CreateOrderDto
    {
        public double total { get; set; }
        public string costumerId { get; set; }
        public string casherId { get; set; }


        public CreateOrderDto() { }
        public CreateOrderDto(double total, string costumerId, string casherId)
        {
            this.total = total;
            this.costumerId = costumerId;
            this.casherId = casherId;
        }
    }

    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.total).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.costumerId).NotEmpty().NotNull();
            RuleFor(x => x.casherId).NotEmpty().NotNull();
        }
    }
}