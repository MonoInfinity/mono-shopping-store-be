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
            AddTranslation("vi", "Message_LoginSuccess", "đăng nhập thành công");
            AddTranslation("vi", "Message_RegisterSuccess", "đăng kí thành công");
            AddTranslation("vi", "Message_LogoutSuccess", "đăng xuất thành công");
            AddTranslation("vi", "Message_UpdateSuccess", "cập nhật thành công");
            AddTranslation("vi", "Message_AddSuccess", "thêm mới thành công");
            AddTranslation("en", "Message_DeleteSuccess", "xóa thành công");

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

            // VI
            AddTranslation("vi", "Error_LoginFail", "username hoặc password không đúng");
            AddTranslation("vi", "Error_Existed", "đã tồn tại");
            AddTranslation("vi", "Error_FailToSaveUser", "lỗi cơ sở dữ liệu");
            AddTranslation("vi", "Error_UpdateFail", "cập nhật không thành công");
            AddTranslation("en", "Error_DeleteFail", "xóa không thành công");
            AddTranslation("vi", "Error_Wrong", "không đúng");
            AddTranslation("vi", "Error_NotFound", "không tìm thấy");
            AddTranslation("vi", "Error_NotAllow", "không có quyền");










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


            AddTranslation("vi", "EmailValidator", "không hợp lệ");
            AddTranslation("vi", "GreaterThanOrEqualValidator", "phải lớn hơn hoặc bằng với {ComparisonValue}");
            AddTranslation("vi", "GreaterThanValidator", "phải lớn hơn {ComparisonValue}");
            AddTranslation("vi", "LengthValidator", "phải nằm trong khoảng từ {MinLength} đến {MaxLength} kí tự");
            AddTranslation("vi", "MinimumLengthValidator", "tối thiểu {MinLength} kí tự");
            AddTranslation("vi", "MaximumLengthValidator", "tối đa  {MaxLength} kí tự");
            AddTranslation("vi", "LessThanOrEqualValidator", "phải nhỏ hơn hoặc bằng {ComparisonValue}");
            AddTranslation("vi", "LessThanValidator", "phải nhỏ hơn {ComparisonValue}");
            AddTranslation("vi", "NotEmptyValidator", "không được rỗng");
            AddTranslation("vi", "NotEqualValidator", "không được bằng {ComparisonValue}");
            AddTranslation("vi", "NotNullValidator", "phải có giá trị");
            AddTranslation("vi", "RegularExpressionValidator", "không đúng định dạng");
            AddTranslation("vi", "EqualValidator", "phải bằng {ComparisonValue}");
            AddTranslation("vi", "ExactLengthValidator", "phải có độ dài chính xác {MaxLength} kí tự");
            AddTranslation("vi", "InclusiveBetweenValidator", "phải có giá trị trong khoảng từ {From} đến {To}");
            AddTranslation("vi", "ExclusiveBetweenValidator", "phải có giá trị trong khoảng giữa {From} và {To}");
            AddTranslation("vi", "EmptyValidator", "phải là rỗng");
            AddTranslation("vi", "NullValidator", "không được chứa giá trị");
            AddTranslation("vi", "EnumValidator", "nằm trong một tập giá trị không bao gồm {PropertyValue}");
        }
    }
}