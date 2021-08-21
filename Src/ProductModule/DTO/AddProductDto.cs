using FluentValidation;

namespace store.Src.ProductModule.DTO
{
    public class AddProductDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string expiryDate { get; set; }
        public double wholesalePrice { get; set; }
        public double retailPrice { get; set; }
        public int quantity { get; set; }
        public string subCategoryId { get; set; }

        public AddProductDto() { }
        public AddProductDto(string name, string description, string location, string expiryDate, double wholesalePrice, double retailPrice, int quantity, string subCategoryId)
        {
            this.name = name;
            this.description = description;
            this.location = location;
            this.expiryDate = expiryDate;
            this.wholesalePrice = wholesalePrice;
            this.retailPrice = retailPrice;
            this.quantity = quantity;
            this.subCategoryId = subCategoryId;
        }
    }

    public class AddProductDtoValidator : AbstractValidator<AddProductDto>
    {
        public AddProductDtoValidator()
        {
            RuleFor(x => x.name).NotEmpty().Length(1, 40).NotNull();
            RuleFor(x => x.description).NotEmpty().Length(1, 500).NotNull();
            RuleFor(x => x.location).NotEmpty().Length(1, 500).NotNull();
            RuleFor(x => x.expiryDate).NotEmpty().NotNull();
            RuleFor(x => x.wholesalePrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.retailPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.quantity).NotEmpty().NotNull().GreaterThan(1);
            RuleFor(x => x.subCategoryId).NotEmpty().NotNull();
        }
    }
}