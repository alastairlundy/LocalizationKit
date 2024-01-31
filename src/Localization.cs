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
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable InconsistentNaming

namespace AlastairLundy.LocalizationKit{
    /// <summary>
    /// A class to represent a Localization
    /// </summary>
    public class Localization {

        /// <summary>
        /// Note: This field may be renamed to LocaleCode in the future once the string LocaleCode field is removed.
        /// </summary>
        public Locale Locale { get; set; }
        
        public string PathToLocalizationFile { get; set; }

        public List<KeyValuePair<string, string>> Translations { get; set; }

        internal SettingsManager<string, string> _settingsManager;
        
        /// <summary>
        /// Create a new Localization object and load Localizations from the ISettingsProvider.
        /// </summary>
        /// <param name="locale">The locale associated with the Localization to be loaded.</param>
        public Localization(Locale locale)
        {
            _settingsManager = new SettingsManager<string, string>();
            Locale = locale; 
        }
        
        /// <summary>
        /// Create a new Localization object and load Localizations from the ISettingsProvider.
        /// </summary>
        /// <param name="locale">The locale associated with the Localization to be loaded.</param>
        /// <param name="pathToFile"></param>
        public Localization(Locale locale, string pathToFile)
        {
            _settingsManager = new SettingsManager<string, string>();
            Locale = locale;
            PathToLocalizationFile = pathToFile;
        }

        /// <summary>
        /// Load the localization
        /// </summary>
        /// <param name="localizationFileProvider">The localizations provider to use to retrieve Localizations stored in a file.</param>
        public void Load(ILocalizationFileProvider localizationFileProvider)
        {
            foreach (var pair in localizationFileProvider.Get(PathToLocalizationFile))
            {
                Translations.Add(pair);
            }
        }

        /// <summary>
        /// Load the localization
        /// </summary>
        /// <param name="localizationFileProvider">The settings provider to use to retrieve Localizations stored in a file.</param>
        /// <param name="pathToLocalizedFile"></param>
        public void Load(ILocalizationFileProvider localizationFileProvider, string pathToLocalizedFile)
        {
            this.PathToLocalizationFile = pathToLocalizedFile;

            foreach (var pair in localizationFileProvider.Get(PathToLocalizationFile))
            {
                Translations.Add(pair);
            }
        }

        /// <summary>
        /// Returns the Localized phrase associated with the specified Key.
        /// </summary>
        /// <param name="key">The key to use when searching for a localized phrase.</param>
        /// <returns>A localized phrase - Usually a translation of a word or words.</returns>
        public KeyValuePair<string, string> GetLocalizedPhrase(string key)
        {
            return _settingsManager.GetKeyValuePair(Translations.ToArray(), key);
        }
    }
}