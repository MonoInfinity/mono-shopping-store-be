using FluentValidation;
using System.Text.RegularExpressions;

namespace store.Src.ProductModule.DTO
{
    public class UpdateImportInfoDto
    {
        public string importInfoId { get; set; }
        public string importDate { get; set; }
        public double importPrice { get; set; }
        public string expiryDate { get; set; }
        public int importQuantity { get; set; }
        public string note { get; set; }
        public string brand { get; set; }

        public UpdateImportInfoDto()
        {
        }
        public UpdateImportInfoDto(string importInfoId, string importDate, double importPrice, string expiryDate, int importQuantity, string note, string brand)
        {
            this.importInfoId = importInfoId;
            this.importDate = importDate;
            this.importPrice = importPrice;
            this.expiryDate = expiryDate;
            this.importQuantity = importQuantity;
            this.note = note;
            this.brand = brand;
        }
    }
    public class UpdateImportInfoDtoValidator : AbstractValidator<UpdateImportInfoDto>
    {
        public UpdateImportInfoDtoValidator()
        {
            RuleFor(x => x.importInfoId).NotEmpty().NotNull();
            RuleFor(x => x.importDate).NotEmpty().NotNull().Matches(new Regex(@"^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4}$"));
            RuleFor(x => x.importPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.expiryDate).NotEmpty().NotNull().Matches(new Regex(@"^(0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4}$"));
            RuleFor(x => x.importQuantity).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.note).NotEmpty().NotNull();
            RuleFor(x => x.brand).NotEmpty().Length(1, 40).NotNull();
        }
    }
}