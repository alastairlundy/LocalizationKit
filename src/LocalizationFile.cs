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
using LocalizationKit.Interfaces;

namespace LocalizationKit;

public class LocalizationFile
{
    protected Dictionary<string, string> Localizations;
    
    public string PathToFile { get; internal set; }
    
    public Locale LocaleCode { get; internal set; }
    
    internal ILocalizationFileProvider FileProvider { get; set; }

    public LocalizationFile(Locale locale, ILocalizationFileProvider localizationFileProvider, string pathToLocalizationFile)
    {
        this.LocaleCode = locale;

        PathToFile = pathToLocalizationFile;
        this.FileProvider = localizationFileProvider;

        Localizations = new Dictionary<string, string>();
        
        foreach (var localizedPhrase in localizationFileProvider.Get(pathToLocalizationFile))
        {
            Localizations.Add(localizedPhrase.Key, localizedPhrase.Value);
        }
    }
    
    /// <summary>
    /// Return the loaded localizations.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> GetLocalizations()
    {
        return Localizations;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public KeyValuePair<string, string> GetKeyValuePair(string key)
    {
        return new KeyValuePair<string, string>(key, GetValue(key));
    }

    /// <summary>
    /// Returns the value associated with the key specified.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetValue(string key)
    {
        return Localizations[key];
    }
}