/* MIT License

Copyright (c) 2023-2024 Alastair Lundy

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

using AlastairLundy.LocalizationKit;
using AlastairLundy.LocalizationKit.Exceptions;

using Azure;
using Azure.AI.Translation.Text;

namespace LocalizationKit.Integrations.Azure;

public static class LocalizationExtensions
{
    public static KeyValuePair<string, string> TranslatePhraseWithAzure(this Localization localization, string azureApiKey, string azureRegion, Locale newLocale, KeyValuePair<string, string> phraseToBeTranslated)
    {
        AzureKeyCredential azureKeyCredential = new(azureApiKey);
        TextTranslationClient client = new(azureKeyCredential, azureRegion);

        if (CheckIfAzureSupportsLocale(client, newLocale))
        {
            Response<IReadOnlyList<TranslatedTextItem>> response = client.Translate(newLocale.LanguageCode, phraseToBeTranslated.Value);
            IReadOnlyList<TranslatedTextItem> translations = response.Value;
            TranslatedTextItem translation = translations.FirstOrDefault() ?? throw new NullReferenceException();

            return new KeyValuePair<string, string>(phraseToBeTranslated.Key,
                translation?.Translations?.FirstOrDefault()?.To ?? throw new NullReferenceException());
        }
        else
        {
            throw new LocaleNotFoundException();
        }
    }
    
    public static bool CheckIfAzureSupportsLocale(TextTranslationClient client, Locale locale)
    {
        //Check if Language is supported by Azure Text Translator API.
        Response<GetLanguagesResult> response = client.GetLanguages(cancellationToken: CancellationToken.None);
        GetLanguagesResult languages = response.Value;

        return languages.Translation.ContainsKey(locale.ToString());
    }
}