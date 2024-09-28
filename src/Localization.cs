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
using System.Linq;
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
        
        public Dictionary<string, string> Phrases { get; internal set; }

        
        /// <summary>
        /// Create a new Localization object.
        /// </summary>
        /// <param name="locale">The locale associated with the Localization to be loaded.</param>
        public Localization(Locale locale)
        {
            LocaleCode = locale;
            Phrases = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// Create a new Localization object and load Localizations.
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="phrases"></param>
        public Localization(Locale locale, KeyValuePair<string, string>[] phrases)
        {
            LocaleCode = locale;
            Phrases = new Dictionary<string, string>();

            AddPhrases(phrases);
        }

        /// <summary>
        ///  Create a new Localization object and load Localizations from the ILocalizationFileProvider.
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="localizationFileProvider"></param>
        /// <param name="pathToFile"></param>
        public Localization(Locale locale, ILocalizationFileProvider localizationFileProvider, string pathToFile)
        {
            LocaleCode = locale;
            Phrases = new Dictionary<string, string>();

           KeyValuePair<string, string>[] data = localizationFileProvider.Get(pathToFile);

           AddPhrases(data);
        }

        /// <summary>
        /// Add a single Key and a single Value to the localizations.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddPhrase(string key, string value)
        {
#if NET5_0_OR_GREATER
            if(!Phrases.TryAdd(key, value))
#else
            if (!Phrases.ContainsKey(key))
#endif
            {
                Phrases.Add(key, value);
            }
        }
        
        /// <summary>
        /// Add a single Key and a single Value to the localizations.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [Obsolete("Use AddPhrase instead")]
        public void Load(string key, string value)
        {
            AddPhrase(key, value);
        }

        /// <summary>
        /// Add a single KeyValuePair to the localizations.
        /// </summary>
        /// <param name="phrase"></param>
        public void AddPhrase(KeyValuePair<string, string> phrase)
        {
#if NET5_0_OR_GREATER
            if(!Phrases.TryAdd(phrase.Key, phrase.Value))
#else
            if (!Phrases.ContainsKey(phrase.Key))
#endif
            {
                Phrases.Add(phrase.Key, phrase.Value);
            }
        }
        
        /// <summary>
        /// Add a single KeyValuePair to the localizations.
        /// </summary>
        /// <param name="phrase"></param>
        [Obsolete("Use AddPhrase instead")]
        public void Load(KeyValuePair<string, string> phrase)
        {
            AddPhrase(phrase);
        }

        /// <summary>
        /// Add a single KeyValuePair to the localizations.
        /// </summary>
        /// <param name="phrases"></param>
        public void AddPhrases(IEnumerable<KeyValuePair<string, string>> phrases)
        {
            foreach (KeyValuePair<string, string> keyValuePair in phrases)
            {
                AddPhrase(keyValuePair);
            }  
        }
        
        /// <summary>
        /// Add an array of Localization KeyValuePairs
        /// </summary>
        /// <param name="localizations"></param>
        [Obsolete("Use AddPhrases instead")]
        public void Load(KeyValuePair<string, string>[] localizations)
        {
           AddPhrases(localizations);
        }

        /// <summary>
        /// Returns the Localized phrase associated with the specified Key.
        /// </summary>
        /// <param name="key">The key to use when searching for a localized phrase.</param>
        /// <param name="ignoreCase">Whether the case of the key should be ignored or not. - Defaults to true if not set.</param>
        /// <returns>A localized phrase - Usually a translation of a word or words.</returns>
        public string GetLocalizedPhrase(string key, bool ignoreCase = true)
        {
            foreach(KeyValuePair<string, string> pair in Phrases) { 
                if(pair.Key.Equals(key, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                {
                    return pair.Value;
                }
            }

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Clears the Localizations stored.
        /// </summary>
        public void Clear()
        {
            Phrases.Clear();
        }
    }
}