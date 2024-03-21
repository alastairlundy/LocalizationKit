/* MIT License

Copyright (c) 2020-2024 Alastair Lundy

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

namespace LocalizationKit{
    /// <summary>
    /// A class to manage Localizations
    /// </summary>
    public class LocalizationManager
    {
        public Dictionary<Locale, Localization> Localizations { get; protected set; }
        
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
        /// Load a Localization
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="localization"></param>
        public void LoadLocalization(Locale locale, Localization localization)
        {
            if (Localizations.ContainsKey(locale)){

                foreach (var keyValuePair in localization.Localizations)
                {
                    Localizations[locale].Load(keyValuePair);
                }
            }
            else
            {
                //Add the localization to the localizations list.
                Localizations.Add(locale, localization);
            }
        }

        /// <summary>
        /// Returns the localization associated with a specified locale.
        /// </summary>
        /// <param name="locale"></param>
        /// <returns></returns>
        public Localization GetLocalization(Locale locale)
        {
            return Localizations[locale];
        }

        /// <summary>
        /// Returns a localized phrase from a localization associated with a specified locale and key.
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="key"></param>
        /// <param name="ignoreCase">Whether the case of the key should be ignored or not. - Defaults to true if not set.</param>
        /// <returns></returns>
        public string GetLocalizedPhrase(Locale locale, string key, bool ignoreCase = true)
        {
            return Localizations[locale].GetLocalizedPhrase(key, ignoreCase);
        }
        

        /// <summary>
        /// Gets a list of Locales Stored in the LocalizationManager
        /// </summary>
        /// <returns></returns>
        public Locale[] GetLocalesStored()
        {
            List<Locale> list = new List<Locale>();
            
            foreach (var localization in Localizations)
            {
                list.Add(localization.Key);
            }

            return list.ToArray();
        }
    }
}