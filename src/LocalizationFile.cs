using System.Collections.Generic;
using LocalizationKit.Interfaces;

namespace LocalizationKit;

public class LocalizationFile
{
    protected Dictionary<string, string> Localizations;
    
    internal string PathToFile { get; set; }
    
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