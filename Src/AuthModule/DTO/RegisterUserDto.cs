using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using store.Src.Utils.Validator;

namespace store.Src.AuthModule.DTO
{
    public class RegisterUserDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public RegisterUserDto()
        {

        }
        public RegisterUserDto(string username, string password, string confirmPassword, string name, string email, string phone, string address, string avatarUrl)
        {
            this.username = username;
            this.password = password;
            this.confirmPassword = confirmPassword;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.address = address;
        }
    }

    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.username).NotEmpty().Length(UserValidator.USERNAME_MIN, UserValidator.USERNAME_MAX);
            RuleFor(x => x.password).NotEmpty().Length(UserValidator.PASSWORD_MIN, UserValidator.PASSWORD_MAX)
            .Custom((value, context)=>{
                bool hasUpperCaseLetter = false ;
                bool hasLowerCaseLetter = false ;
                bool hasDecimalDigit = false ;
                bool hasWhiteSpace = false;

                foreach(char c in value){
                    if(char.IsUpper(c)) hasUpperCaseLetter = true;
                    if(char.IsLower(c)) hasLowerCaseLetter = true;
                    if(char.IsDigit(c)) hasDecimalDigit = true;
                    if(char.IsWhiteSpace(c)) hasWhiteSpace = true;
                }

                if(!hasDecimalDigit || !hasLowerCaseLetter || !hasUpperCaseLetter){
                    string errorMessage = ValidatorOptions.Global.LanguageManager.GetString("Error_PasswordNotContainRequiredCharacter");
                    context.AddFailure(new ValidationFailure("password", errorMessage));
                }

                if(hasWhiteSpace){
                    string errorMessage = ValidatorOptions.Global.LanguageManager.GetString("Error_PasswordContainWhiteSpace");
                    context.AddFailure(new ValidationFailure("password", errorMessage));
                }
            });
            RuleFor(x => x.confirmPassword).NotEmpty().Equal(x => x.password);
            RuleFor(x => x.name).NotEmpty().Length(UserValidator.NAME_MIN, UserValidator.NAME_MAX);
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.phone).NotEmpty().NotNull().Matches(new Regex(@"^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b"));
            RuleFor(x => x.address).NotEmpty().Length(UserValidator.ADDRESS_MIN, UserValidator.ADDRESS_MAX);
        }
    }
}