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

using System;
using System.Collections.Generic;
using LocalizationKit.Interfaces;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable InconsistentNaming

namespace LocalizationKit{
    
    /// <summary>
    /// A class to represent a Localization
    /// </summary>
    public class Localization {

        /// <summary>
        ///
        /// </summary>
        public Locale LocaleCode { get; internal set; }
        
        public List<LocalizationFile> LocalizationFiles { get; internal set; }
        
        public Dictionary<string, string> Localizations { get; internal set; }

        
        /// <summary>
        /// Create a new Localization object and load Localizations from the ISettingsProvider.
        /// </summary>
        /// <param name="locale">The locale associated with the Localization to be loaded.</param>
        public Localization(Locale locale)
        {
            LocaleCode = locale;
            Localizations = new Dictionary<string, string>();
        }

        /// <summary>
        /// Create a new Localization object and load Localizations from the ISettingsProvider.
        /// </summary>
        /// <param name="locale">The locale associated with the Localization to be loaded.</param>
        /// <param name="localizationFiles"></param>
        public Localization(Locale locale, LocalizationFile[] localizationFiles)
        {
            LocaleCode = locale;
            Localizations = new Dictionary<string, string>();
            Load(localizationFiles);
        }

        /// <summary>
        /// Add a single KeyValuePair to the localizations.
        /// </summary>
        /// <param name="localization"></param>
        public void Load(KeyValuePair<string, string> localization)
        {
            if (!Localizations.ContainsKey(localization.Key))
            {
                Localizations.Add(localization.Key, localization.Value);
            }
        }
        
        /// <summary>
        /// Add an array of Localization KeyValuePairs
        /// </summary>
        /// <param name="localizations"></param>
        public void Load(KeyValuePair<string, string>[] localizations)
        {
            foreach (var keyValuePair in localizations)
            {
                Load(keyValuePair);
            }
        }
        
        /// <summary>
        /// Loads a single localization file.
        /// </summary>
        /// <param name="localizationFile"></param>
        public void Load(LocalizationFile localizationFile)
        {
            Load(new LocalizationFile[]{ localizationFile});
        }
        /// </summary>
        public void Load(LocalizationFile[] localizationFiles)
        {
            foreach (LocalizationFile localizationFile in localizationFiles)
            {
                if (localizationFile.LocaleCode.ToString().Equals(LocaleCode.ToString()))
                {
                    foreach (KeyValuePair<string, string> localizedPhrase in localizationFile.GetLocalizations())
                    {
                        Localizations.Add(localizedPhrase.Key, localizedPhrase.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the Localized phrase associated with the specified Key.
        /// </summary>
        /// <param name="key">The key to use when searching for a localized phrase.</param>
        /// <param name="ignoreCase">Whether the case of the key should be ignored or not. - Defaults to true if not set.</param>
        /// <returns>A localized phrase - Usually a translation of a word or words.</returns>
        public string GetLocalizedPhrase(string key, bool ignoreCase = true)
        {
            foreach(var pair in Localizations) { 
                if(pair.Key.Equals(key, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                {
                    return pair.Value;
                }
            }

            throw new KeyNotFoundException();
        }
    }
}