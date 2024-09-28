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

#nullable enable
using System;

namespace LocalizationKit;

/// <summary>
/// A class to represent Locales.
/// </summary>
public class Locale
{
    public string SeparatorCharacterToUse { get; set; }
    
    /// <summary>
    /// A 2 digit language code indicating which language the locale belongs to (e.g. en_us uses the en language code).
    /// </summary>
    public string LanguageCode { get; set; }
    
    /// <summary>
    /// A 2 digit country code that represents the country where the locale belongs to within the language support. (e.g. en_us is US English, en_gb is British English, fr_fr is French french).
    /// </summary>
    public string? CountryCode { get; set; }

    /// <summary>
    /// Returns the Locale to string in the format of LanguageCode_CountryCode.
    /// The results are always lower case.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (CountryCode is null)
        {
            return LanguageCode.ToLower();
        }
        else
        {
            return $"{LanguageCode}{SeparatorCharacterToUse}{CountryCode}".ToLower();
        }
    }
    
    

    public void Parse(string locale)
    {
#if NET6_0_OR_GREATER
            if (locale.Contains('_'))
            {
                string[] adjustedLocale = locale.Split('_');

                LanguageCode = adjustedLocale[0].ToLower();

                if (adjustedLocale[1].Equals(SeparatorCharacterToUse))
                {
                    CountryCode = adjustedLocale.Length > 2 ? adjustedLocale[2].ToLower() : "";
                }
                else
                {
                    CountryCode = adjustedLocale[1].ToLower();
                }
            }
#else
            if (locale.Contains("_"))
            {
                string[] adjustedLocale = locale.Split(SeparatorCharacterToUse[0]);

                LanguageCode = adjustedLocale[0].ToLower();

                if (adjustedLocale[1].Equals(SeparatorCharacterToUse))
                {
                    if (adjustedLocale.Length > 2)
                    {
                        CountryCode = adjustedLocale[2].ToLower();
                    }
                    else
                    {
                        CountryCode = "";
                    }
                }
                else
                {
                    CountryCode = adjustedLocale[1].ToLower();
                }
            }
#endif
            else
            {
                if(locale.Length == 2)
                {
                    LanguageCode = locale;
                    CountryCode = null;
                }
            }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="locale"></param>
    public Locale(string locale)
    {
        LanguageCode = "";
        Parse(locale);
        SeparatorCharacterToUse = "-";
    }
    
    /// <summary>
    /// Initialize the Locale upon instantiation.
    /// </summary>
    /// <param name="languageCode"></param>
    /// <param name="countryCode"></param>
    /// <param name="separatorCharacterToUse"></param>
    public Locale(string languageCode, string countryCode, string separatorCharacterToUse)
    {
        LanguageCode = languageCode;
        CountryCode = countryCode;
        SeparatorCharacterToUse = separatorCharacterToUse;
    }
}