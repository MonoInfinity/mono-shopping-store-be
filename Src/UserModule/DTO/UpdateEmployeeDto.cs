using FluentValidation;

namespace store.Src.UserModule.DTO
{
    public class UpdateEmployeeDto
    {
        public string userId { get; set; }
        public int role { get; set; }
        public double salary { get; set; }
        public int status { get; set; }
        public UpdateEmployeeDto(string userId, int role, double salary, int userStatus)
        {
            this.userId = userId;
            this.role = role;
            this.salary = salary;
            this.status = status;
        }

        public UpdateEmployeeDto()
        {

        }
    }

    public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeDtoValidator()
        {
            RuleFor(x => x.userId).NotEmpty().Length(30, 40);
            RuleFor(x => x.role).NotNull().InclusiveBetween(1, 5);
            RuleFor(x => x.salary).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.status).NotNull().InclusiveBetween(0, 1);
        }
    }
}