namespace store.Src.Utils.Locale
{
    public class CustomLanguageValidator : FluentValidation.Resources.LanguageManager
    {
        public static class ErrorMessageKey {
            public const string Error_LoginFail = "Error_LoginFail";
            public const string Error_Existed = "Error_Existed";
            public const string Error_FailToSave = "Error_FailToSave";
            public const string Error_UpdateFail = "Error_UpdateFail";
            public const string Error_DeleteFail = "Error_DeleteFail";
            public const string Error_Wrong = "Error_Wrong";
            public const string Error_NotFound = "Error_NotFound";
            public const string Error_NotAllow = "Error_NotAllow";
            public const string Error_PasswordNotContainRequiredCharacter = "Error_PasswordNotContainRequiredCharacter";
            public const string Error_PasswordContainWhiteSpace = "Error_PasswordContainWhiteSpace";
        }

        public static class MessageKey {
            public const string Message_LoginSuccess = "Message_LoginSuccess";
            public const string Message_RegisterSuccess = "Message_RegisterSuccess";
            public const string Message_LogoutSuccess = "Message_LogoutSuccess";
            public const string Message_UpdateSuccess = "Message_UpdateSuccess";
            public const string Message_AddSuccess = "Message_AddSuccess";
            public const string Message_DeleteSuccess = "Message_DeleteSuccess";
        }
        public CustomLanguageValidator()
        {
            
            // Success message
            // EN
            AddTranslation("en", "Message_LoginSuccess", "login success");
            AddTranslation("en", "Message_RegisterSuccess", "register success");
            AddTranslation("en", "Message_LogoutSuccess", "logout success");
            AddTranslation("en", "Message_UpdateSuccess", "update success");
            AddTranslation("en", "Message_AddSuccess", "add success");
            AddTranslation("en", "Message_DeleteSuccess", "delete success");

            // VI
            AddTranslation("vi", "Message_LoginSuccess", "????ng nh???p th??nh c??ng");
            AddTranslation("vi", "Message_RegisterSuccess", "????ng k?? th??nh c??ng");
            AddTranslation("vi", "Message_LogoutSuccess", "????ng xu???t th??nh c??ng");
            AddTranslation("vi", "Message_UpdateSuccess", "c???p nh???t th??nh c??ng");
            AddTranslation("vi", "Message_AddSuccess", "th??m m???i th??nh c??ng");
            AddTranslation("vi", "Message_DeleteSuccess", "x??a th??nh c??ng");

            // Error message
            // EN
            AddTranslation("en", "Error_LoginFail", "username or password is wrong");
            AddTranslation("en", "Error_Existed", "is already exist");
            AddTranslation("en", "Error_FailToSave", "database error");
            AddTranslation("en", "Error_UpdateFail", "update fail");
            AddTranslation("en", "Error_DeleteFail", "delete fail");
            AddTranslation("en", "Error_Wrong", "is wrong");
            AddTranslation("en", "Error_NotFound", "is not found");
            AddTranslation("en", "Error_NotAllow", "not allow");
            AddTranslation("en", "Error_PasswordNotContainRequiredCharacter", "should contain at least 1 uppercase, 1 lowwercase, 1 number");
            AddTranslation("en", "Error_PasswordContainWhiteSpace", "should not contain white space");

            // VI
            AddTranslation("vi", "Error_LoginFail", "username ho???c password kh??ng ????ng");
            AddTranslation("vi", "Error_Existed", "???? t???n t???i");
            AddTranslation("vi", "Error_FailToSaveUser", "l???i c?? s??? d??? li???u");
            AddTranslation("vi", "Error_UpdateFail", "c???p nh???t kh??ng th??nh c??ng");
            AddTranslation("vi", "Error_DeleteFail", "x??a kh??ng th??nh c??ng");
            AddTranslation("vi", "Error_Wrong", "kh??ng ????ng");
            AddTranslation("vi", "Error_NotFound", "kh??ng t??m th???y");
            AddTranslation("vi", "Error_NotAllow", "kh??ng c?? quy???n");
            AddTranslation("vi", "Error_PasswordNotContainRequiredCharacter", "ph???i c?? ??t nh???t 1 k?? t??? hoa, 1 k?? t??? th?????ng, 1 s???");
            AddTranslation("vi", "Error_PasswordContainWhiteSpace", "kh??ng ???????c ch???a kho???ng tr???ng");










            // Don't touch me please
            AddTranslation("en", "EmailValidator", "is not a valid email address");
            AddTranslation("en", "GreaterThanOrEqualValidator", "should be greater than or equal to {ComparisonValue}");
            AddTranslation("en", "GreaterThanValidator", "should be greater than {ComparisonValue}");
            AddTranslation("en", "LengthValidator", "should be between {MinLength} and {MaxLength} characters");
            AddTranslation("en", "MinimumLengthValidator", "should be at least {MinLength} characters");
            AddTranslation("en", "MaximumLengthValidator", "should be {MaxLength} characters or fewer");
            AddTranslation("en", "LessThanOrEqualValidator", "should be less than or equal to {ComparisonValue}");
            AddTranslation("en", "LessThanValidator", "should be less than {ComparisonValue}");
            AddTranslation("en", "NotEmptyValidator", "should not be empty");
            AddTranslation("en", "NotEqualValidator", "should not be equal to {ComparisonValue}");
            AddTranslation("en", "NotNullValidator", "should not be empty");
            AddTranslation("en", "RegularExpressionValidator", "is not in the correct format");
            AddTranslation("en", "EqualValidator", "should be equal to {ComparisonValue}");
            AddTranslation("en", "ExactLengthValidator", "should be equal to {ComparisonValue}");
            AddTranslation("en", "InclusiveBetweenValidator", "should be between {From} and {To}");
            AddTranslation("en", "ExclusiveBetweenValidator", "should be between {From} and {To} (exclusive)");
            AddTranslation("en", "NullValidator", "must be empty");
            AddTranslation("en", "EmptyValidator", "must be empty");
            AddTranslation("en", "EnumValidator", "has a range of values which does not include {PropertyValue}");


            AddTranslation("vi", "EmailValidator", "kh??ng h???p l???");
            AddTranslation("vi", "GreaterThanOrEqualValidator", "ph???i l???n h??n ho???c b???ng v???i {ComparisonValue}");
            AddTranslation("vi", "GreaterThanValidator", "ph???i l???n h??n {ComparisonValue}");
            AddTranslation("vi", "LengthValidator", "ph???i n???m trong kho???ng t??? {MinLength} ?????n {MaxLength} k?? t???");
            AddTranslation("vi", "MinimumLengthValidator", "t???i thi???u {MinLength} k?? t???");
            AddTranslation("vi", "MaximumLengthValidator", "t???i ??a  {MaxLength} k?? t???");
            AddTranslation("vi", "LessThanOrEqualValidator", "ph???i nh??? h??n ho???c b???ng {ComparisonValue}");
            AddTranslation("vi", "LessThanValidator", "ph???i nh??? h??n {ComparisonValue}");
            AddTranslation("vi", "NotEmptyValidator", "kh??ng ???????c r???ng");
            AddTranslation("vi", "NotEqualValidator", "kh??ng ???????c b???ng {ComparisonValue}");
            AddTranslation("vi", "NotNullValidator", "ph???i c?? gi?? tr???");
            AddTranslation("vi", "RegularExpressionValidator", "kh??ng ????ng ?????nh d???ng");
            AddTranslation("vi", "EqualValidator", "ph???i b???ng {ComparisonValue}");
            AddTranslation("vi", "ExactLengthValidator", "ph???i c?? ????? d??i ch??nh x??c {MaxLength} k?? t???");
            AddTranslation("vi", "InclusiveBetweenValidator", "ph???i c?? gi?? tr??? trong kho???ng t??? {From} ?????n {To}");
            AddTranslation("vi", "ExclusiveBetweenValidator", "ph???i c?? gi?? tr??? trong kho???ng gi???a {From} v?? {To}");
            AddTranslation("vi", "EmptyValidator", "ph???i l?? r???ng");
            AddTranslation("vi", "NullValidator", "kh??ng ???????c ch???a gi?? tr???");
            AddTranslation("vi", "EnumValidator", "n???m trong m???t t???p gi?? tr??? kh??ng bao g???m {PropertyValue}");
        }
    }
}