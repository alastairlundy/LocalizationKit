/* BSD 3-Clause License

Copyright (c) 2020, AluminiumTech
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its
   contributors may be used to endorse or promote products derived from
   this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;

using AluminiumTech.DevKit;
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
          KeyValuePair<string, string> localization = new KeyValuePair<string, string>(LOCALE, LOCALIZATION_DIRECTORY);
          Add(localization);
        }
        
        /// <summary>
        /// Remove a localization
        /// </summary>
        /// <param name="LOCALE"></param>
        public void RemoveLocalization(string LOCALE)
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
    }
}