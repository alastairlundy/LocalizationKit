
using AluminiumTech.SettingsKit.Base;

namespace AluminiumTech.LocalizationKit
{
    public class LocalizationManager : Preferences<string, Localization> 
    {

        public LocalizationManager()
        {
            
        }


        public Localization GetLocalization(string LOCALE)
        {
            for (int i = 0; i < _preferences.Count; i++)
            {
                if (_preferences[i].KEY.Equals(LOCALE)){
                    
                }
            }
        }

        public void CreateLocalization(Localization localization)
        {
            Preferences<string, Localization> pref = new Preferences<string, Localization>();
        

        }

        public void RemoveLocalization(Localization localization)
        {
            Remove(Get)
        }
    }
}