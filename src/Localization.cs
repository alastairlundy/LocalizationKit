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
    /// A class to represent a Localization
    /// </summary>
    public class Localization {

        public string LocaleCode { get; set; }
        public string Language { get; set; }

        public string PathToLocalizationJsonFile { get; set; }

        public SettingsManager<string, string> Translations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathToJsonFile"></param>
        /// <param name="localeCode"></param>
        public Localization(string pathToJsonFile, string localeCode)
        {
            LocaleCode = localeCode;
            Language = "";
            PathToLocalizationJsonFile = pathToJsonFile;

            Translations = new SettingsManager<string, string>(pathToJsonFile);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> GetLocalizedPhrase(string key)
        {
            return Translations.Get(key);
        }
    }
}