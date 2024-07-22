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

using System;
using System.Collections.Generic;
using System.Linq;

using Azure;
using Azure.AI.Translation.Text;

using LocalizationKit.Exceptions;

namespace LocalizationKit.Integrations.Azure;

public static class StringExtensions
{
    /// <summary>
    /// Translates a phrase using Azure's AI Text translation.
    /// </summary>
    /// <param name="phraseToBeTranslated">The phrase to be translated.</param>
    /// <param name="azureApiKey">The Azure API Key to be used to access Azure.</param>
    /// <param name="azureRegion">The Azure Region to be used to perform the translation task.</param>
    /// <param name="newLocale">The locale to be translated to.</param>
    /// <returns>the name of the key and the translated value.</returns>
    /// <exception cref="NullReferenceException">Thrown if no translation is provided by Azure.</exception>
    /// <exception cref="LocaleNotFoundException">Thrown if the locale is not supported by Azure.</exception>
    public static string TranslatePhraseWithAzure(this string phraseToBeTranslated, string azureApiKey, string azureRegion, Locale newLocale)
    {
        AzureKeyCredential azureKeyCredential = new AzureKeyCredential(azureApiKey);
        TextTranslationClient client = new(azureKeyCredential, azureRegion);

        if (newLocale.CheckIfAzureSupportsLocale(client))
        {
            Response<IReadOnlyList<TranslatedTextItem>> response = client.Translate(newLocale.LanguageCode, phraseToBeTranslated);
            IReadOnlyList<TranslatedTextItem> translations = response.Value;
            TranslatedTextItem translation = translations.FirstOrDefault() ?? throw new NullReferenceException();

            return translation?.Translations?.FirstOrDefault()?.ToString() ?? throw new NullReferenceException();
        }
        else
        {
            throw new LocaleNotFoundException();
        }
    }
}