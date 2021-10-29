/* MIT License

Copyright (c) 2020-2021 AluminiumTech DevKit

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System.Collections.Generic;

using AluminiumTech.DevKit.SettingsKit;

namespace AluminiumTech.DevKit.LocalizationKit{
    /// <summary>
    /// A class to manage Localizations
    /// </summary>
    public class LocalizationManager
    {
        protected Dictionary<string, Localization> Localizations;
        
        public LocalizationManager()
        {
            Localizations = new Dictionary<string, Localization>();
        }

        public void LoadLocalization(string locale, string pathToLocalizationJsonFile)
        {
            var localization = new Localization(pathToLocalizationJsonFile, locale);
            localization.PreferencesManager = new PreferencesManager<string, string>(pathToLocalizationJsonFile);
            localization.PreferencesManager.LoadPreferences(pathToLocalizationJsonFile);   
            
            Localizations.Add(locale, localization);
        }

        public Localization GetLocalization(string locale)
        {
            return Localizations[locale];
        }

        public Preference<string, string> GetLocalizedPhrase(string locale, string key)
        {
            return Localizations[locale].GetLocalizedPhrase(key);
        }

        public Dictionary<string, Localization> ToLocalizations()
        {
            return Localizations;
        }
    }
}