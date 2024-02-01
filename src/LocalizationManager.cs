/* MIT License

Copyright (c) 2020-2023 Alastair Lundy

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

using System;
using System.Collections.Generic;
using AlastairLundy.LocalizationKit.Interfaces;
using AlastairLundy.SettingsKit;

namespace AlastairLundy.LocalizationKit{
    /// <summary>
    /// A class to manage Localizations
    /// </summary>
    public class LocalizationManager
    {
        protected Dictionary<Locale, Localization> Localizations;
        
        public bool IsCaseSensitive { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enableCaseSensitivity"></param>
        public LocalizationManager(bool enableCaseSensitivity = true)
        {
            IsCaseSensitive = enableCaseSensitivity;
            
            Localizations = new Dictionary<Locale, Localization>();
        }

        /// <summary>
        /// Load localizations from a file using an ISettings Provider
        /// </summary>
        /// <param name="locale">The locale of the localization to add</param>
        /// <param name="pathToLocalizationFile">The path to the localization file.</param>
        /// <param name="localizationFileProvider">The provider to use</param>
        public void LoadLocalization(Locale locale, string pathToLocalizationFile, ILocalizationFileProvider localizationFileProvider)
        {
            Localization localization = new Localization(locale);

            localization.Load(localizationFileProvider, pathToLocalizationFile);

            //Add the localization to the localizations list.
            Localizations.Add(locale, localization);
        }

        /// <summary>
        /// Returns the localization associated with a specified locale.
        /// </summary>
        /// <param name="locale"></param>
        /// <returns></returns>
        public Localization GetLocalization(string locale)
        {
            if (IsCaseSensitive)
            {
                return Localizations[new Locale(locale)];
            }
            else
            {
                return Localizations[new Locale(locale)];
            }
        }

        /// <summary>
        /// Returns a localized phrase from a localization associated with a specified locale and key.
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("This function is deprecated as it relies on a deprecated parameter.")]
        public KeyValuePair<string, string> GetLocalizedPhrase(string locale, string key)
        {
            if (IsCaseSensitive)
            {
                return Localizations[new Locale(locale)].GetLocalizedPhrase(key);
            }
            else
            {
                return Localizations[new Locale(locale.ToLower())].GetLocalizedPhrase(key);
            }
        }
        /// <summary>
        /// Returns a localized phrase from a localization associated with a specified locale and key.
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> GetLocalizedPhrase(Locale locale, string key)
        {
            if (IsCaseSensitive)
            {
                return Localizations[locale].GetLocalizedPhrase(key);
            }
            else
            {
                return Localizations[locale].GetLocalizedPhrase(key);
            }
        }

        /// <summary>
        /// Return all localizations as a Dictionary.
        /// </summary>
        /// <returns></returns>
        [Obsolete("This function is deprecated as it relies on deprecated code and will be removed in a future version.")]
        public Dictionary<string, Localization> ToLocalizations()
        {
            Dictionary<string, Localization> newLocalizations = new Dictionary<string, Localization>();

            foreach (var localization in Localizations)
            {
                newLocalizations.Add(localization.Key.ToString(), localization.Value);
            }

            return newLocalizations;
        }
        
        /// <summary>
        /// Return all localizations as a Dictionary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Locale, Localization> ToDictionary()
        {
            return Localizations;
        }
        

        /// <summary>
        /// Gets a list of Locales Stored in the LocalizationManager
        /// </summary>
        /// <returns></returns>
        public string[] GetLocalesStored()
        {
            List<string> list = new List<string>();
            
            foreach (var localization in Localizations)
            {
                list.Add(localization.Key.ToString());
            }

            return list.ToArray();
        }
    }
}