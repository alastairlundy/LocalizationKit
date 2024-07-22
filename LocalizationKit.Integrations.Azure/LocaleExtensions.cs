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

using Azure;
using Azure.AI.Translation.Text;

namespace LocalizationKit.Integrations.Azure;

public static class LocaleExtensions
{
    /// <summary>
    /// Checks to see if a locale is supported by Azure's AI Text Translation service.
    /// </summary>
    /// <param name="locale">The locale to checked for support.</param>
    /// <param name="client"></param>
    /// <returns>true if a locale is supported by Azure; returns false otherwise.</returns>
    public static bool CheckIfAzureSupportsLocale(this Locale locale, TextTranslationClient client)
    {
        //Check if Language is supported by Azure Text Translator API.
        Response<GetSupportedLanguagesResult> response = client.GetSupportedLanguages();
       
        GetSupportedLanguagesResult languages = response.Value;

        return languages.Translation.ContainsKey(locale.ToString());
    }
}