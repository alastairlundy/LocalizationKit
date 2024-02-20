using System.Collections.Generic;
using LocalizationKit.Interfaces;

namespace LocalizationKit;

public class LocalizationFile
{
    public LocalizationFile(Locale locale){
        this.locale = locale;
    }

    public LocalizationFile(Locale locale, ILocalizationFileProvider localizationFileProvider, string pathToLocalizationFile)
    {
        this.locale = locale;
        Load(localizationFileProvider, pathToLocalizationFile);
    }
    
    public Locale locale { get; }

    public void Load(ILocalizationFileProvider localizationFileProvider, string pathToLocalizationFile)
    {
        
    }

    public KeyValuePair<string, string>[] Get()
    {
        
    }

    public string Get(string key)
    {
        
    }

    internal void SaveFile()
    {
        
    }
}