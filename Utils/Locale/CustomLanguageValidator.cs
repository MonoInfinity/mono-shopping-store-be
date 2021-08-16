namespace store.Utils.Locale
{
    public class CustomLanguageValidator : FluentValidation.Resources.LanguageManager
    {
        public CustomLanguageValidator()
        {
            AddTranslation("en", "NotNullValidator", "'{PropertyName}' is DSADS.");
            AddTranslation("fr", "NotNullValidator", "'{PropertyName}' is TEST.");
        }
    }
}