using System.Text.RegularExpressions;
using FluentValidation;

namespace store.Src.ProductModule.DTO
{
    public class AddImportInfoDto
    {
        public string importDate { get; set; }
        public double importPrice { get; set; }
        public string expiryDate { get; set; }
        public int importQuantity { get; set; }
        public string note { get; set; }
        public string brand { get; set; }
        public string managerId { get; set; }

        public AddImportInfoDto(){}
        public AddImportInfoDto(string importDate, double importPrice, string expiryDate, int importQuantity, string note, string brand, string managerId){
            this.importDate = importDate;
            this.importPrice = importPrice;
            this.expiryDate = expiryDate;
            this.importQuantity = importQuantity;
            this.note = note;
            this.brand = brand;
            this.managerId = managerId;
        }
    }

    public class AddImportInfoDtoValidator: AbstractValidator<AddImportInfoDto>
    {
        public AddImportInfoDtoValidator()
        {
            RuleFor(x => x.importDate).NotEmpty().NotNull().Matches(new Regex(@"^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"));
            RuleFor(x => x.importPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.expiryDate).NotEmpty().NotNull().Matches(new Regex(@"^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"));
            RuleFor(x => x.importQuantity).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.note).NotEmpty().NotNull();
            RuleFor(x => x.brand).NotEmpty().Length(1, 40).NotNull();
            RuleFor(x => x.managerId).NotEmpty().NotNull();
        }
    }
}