/* MIT License

Copyright (c) 2021 AluminiumTech DevKit

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

using AluminiumTech.SettingsKit;
using AluminiumTech.SettingsKit.Base;

namespace AluminiumTech.LocalizationKit
{
    /// <summary>
    /// A class to manage Localizations
    /// </summary>
    public class LocalizationManager : Preferences<string, string> 
    {
        
        /// <summary>
        /// Get the array of Localizations
        /// </summary>
        /// <param name="LOCALES"></param>
        /// <returns></returns>
        public Localization[] ToLocalizationArray(string[] LOCALES)
        {
            return ToLocalizationList(LOCALES).ToArray();
        }
        
        /// <summary>
        /// Get the list of Localizations
        /// </summary>
        /// <param name="LOCALES"></param>
        /// <returns></returns>
        public List<Localization> ToLocalizationList(string[] LOCALES)
        {
            List<Localization> localizations = new List<Localization>();
            for (int index = 0; index < this.Count; index++)
            {
                localizations.Add(GetLocalization(LOCALES[index]));
            }

            return localizations;
        }
        
        /// <summary>
        /// Get the localization
        /// </summary>
        /// <param name="LOCALE"></param>
        /// <returns></returns>
        public Localization GetLocalization(string LOCALE)
        {
            try
            {
                KeyValuePair<string, string> preference;

                int index = 0;
                foreach (KeyValuePair<string, string> pairs in this)
                {
                    if (pairs.Key.Equals(LOCALE))
                    {
                        preference = this[index];
                    }

                    index++;
                }

                PreferencesReader<string, string> reader = new PreferencesReader<string, string>(preference.Value);
                return reader.GetPreferences() as Localization;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
        
        /// <summary>
        /// Add localizations
        /// </summary>
        /// <param name="LOCALE"></param>
        /// <param name="LOCALIZATION_DIRECTORY"></param>
        public void CreateLocalization(string LOCALE, string LOCALIZATION_DIRECTORY)
        {
            try
            {
                KeyValuePair<string, string> localization = new KeyValuePair<string, string>(LOCALE, LOCALIZATION_DIRECTORY);
                Add(localization);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
        
        /// <summary>
        /// Remove a localization
        /// </summary>
        /// <param name="LOCALE"></param>
        public void RemoveLocalization(string LOCALE)
        {
            try
            {
                KeyValuePair<string, string> localizationToBeRemoved;
                foreach (KeyValuePair<string, string> localizations in this)
                {
                    if (localizations.Key.Equals(LOCALE))
                    {
                        localizationToBeRemoved = new KeyValuePair<string, string>(localizations.Key, localizations.Value);
                    }
                }

                this.Remove(localizationToBeRemoved);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
    }
}