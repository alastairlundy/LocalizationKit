namespace AlastairLundy.LocalizationKit;

/// <summary>
/// A class to represent Locales.
/// </summary>
public class Locale
{
    /// <summary>
    /// A 2 digit language code indicating which language the locale belongs to (e.g. en_us uses the en language code).
    /// </summary>
    public string LanguageCode { get; set; }
    
    /// <summary>
    /// A 2 digit country code that represents the country where the locale belongs to within the language support. (e.g. en_us is US English, en_gb is British English, fr_fr is French french).
    /// </summary>
    public string CountryCode { get; set; }

    /// <summary>
    /// Returns the Locale to string in the format of LanguageCode_CountryCode.
    /// The results are always lower case.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{LanguageCode}_{CountryCode}".ToLower();
    }

    /// <summary>
    /// Initialize the Locale upon instantiation.
    /// </summary>
    /// <param name="languageCode"></param>
    /// <param name="countryCode"></param>
    public Locale(string languageCode, string countryCode)
    {
        LanguageCode = languageCode;
        CountryCode = countryCode;
    }
}