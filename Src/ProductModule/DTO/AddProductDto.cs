using System.Text.RegularExpressions;
using FluentValidation;

namespace store.Src.ProductModule.DTO
{
    public class AddProductDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public double wholesalePrice { get; set; }
        public double retailPrice { get; set; }
        public int quantity { get; set; }
        public string imageUrl { get; set; }
        public string subCategoryId { get; set; }
        public string importInfoId { get; set; }

        public AddProductDto() { }
        public AddProductDto(string name, string description, string location, double wholesalePrice, double retailPrice, int quantity, string imageUrl, string subCategoryId, string importInfoId)
        {
            this.name = name;
            this.description = description;
            this.location = location;
            this.wholesalePrice = wholesalePrice;
            this.retailPrice = retailPrice;
            this.quantity = quantity;
            this.subCategoryId = subCategoryId;
            this.importInfoId = importInfoId;
            this.imageUrl = imageUrl;
        }
    }

    public class AddProductDtoValidator : AbstractValidator<AddProductDto>
    {
        public AddProductDtoValidator()
        {
            RuleFor(x => x.name).NotEmpty().Length(1, 40).NotNull();
            RuleFor(x => x.description).NotEmpty().Length(1, 500).NotNull();
            RuleFor(x => x.location).NotEmpty().Length(1, 500).NotNull();
            RuleFor(x => x.wholesalePrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.retailPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.quantity).NotEmpty().NotNull().GreaterThan(1);
            RuleFor(x => x.subCategoryId).NotEmpty().NotNull();
            RuleFor(x => x.importInfoId).NotEmpty().NotNull();
            RuleFor(x => x.imageUrl).NotEmpty().NotNull();
        }
    }
}