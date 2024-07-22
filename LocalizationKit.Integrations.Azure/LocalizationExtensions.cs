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

using System.Collections.Generic;

namespace LocalizationKit.Integrations.Azure;

public static class LocalizationExtensions
{
    /// <summary>
    /// Uses Azure's AI powered cloud translation service to translate a Localization to a specified Locale.
    /// </summary>
    /// <param name="localizationToUse">The localization to be translated.</param>
    /// <param name="azureApiKey">Your Azure API key for the AI powered translation service.</param>
    /// <param name="azureRegion">The Azure Region you are using for this.</param>
    /// <param name="newLocale">The locale to be translated to.</param>
    /// <returns>the translated Localization object.</returns>
    public static Localization TranslateWithAzure(this Localization localizationToUse, string azureApiKey, string azureRegion, Locale newLocale)
    {
        Localization newLocalization = new Localization(newLocale);

        foreach (KeyValuePair<string, string> translation in localizationToUse.Phrases)
        {
            newLocalization.Phrases.Add(translation.Key, translation.Value.TranslatePhraseWithAzure(azureApiKey, azureRegion, newLocale));
        }

        return newLocalization;
    }
}