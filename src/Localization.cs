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

using AluminiumTech.DevKit.SettingsKit.Base;

namespace AluminiumTech.LocalizationKit{
    /// <summary>
    /// A class to represent a Localization
    /// </summary>
    public class Localization{

        public string LOCALE_CODE { get; set; }
        public string LANGUAGE { get; set; }

        public string PathToJsonFile { get; set; }

        public Preferences<string, string> Preferences{ get; set;}

        /// <summary>
        /// Temporary scaffolding to make transitioning from V1 easier.
        /// This will be removed in a future 2.x or 3.x release
        /// </summary>
        public Preferences<string, string> GetPreferences() => Preferences;

        public Localization()
        {
            LOCALE_CODE = "";
            LANGUAGE = "";
            PathToJsonFile = "";
            Preferences = new Preferences<string, string>();
        }
    }
}