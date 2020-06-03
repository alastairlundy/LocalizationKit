
using AluminiumTech.SettingsKit.Base;

namespace AluminiumTech.LocalizationKit
{
    public class Localization : Preferences<string, string>
    {
        protected string LOCALE_CODE;
        protected string LANGUAGE;
        public Localization(string LOCALE_CODE, string LANGUAGE)
        {
            this.LOCALE_CODE = LOCALE_CODE;
            this.LANGUAGE = LANGUAGE;
        }

        public string GetLocale()
        {
            return LOCALE_CODE;
        }

        public string GetLanguage()
        {
            return LANGUAGE;
        }
    }
}