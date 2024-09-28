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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using LocalizationKit.Interfaces;

namespace LocalizationKit.Providers;

/// <summary>
/// A class to read and write Localizations to/from Text files.
/// </summary>
public class TextLocalizationFileProvider : ILocalizationFileProvider
{
    /// <summary>
    /// Retrieves string Keys and Values stored in a .txt Text File.
    /// </summary>
    /// <param name="pathToFile"></param>
    /// <returns></returns>
    public IEnumerable<KeyValuePair<string, string>> Get(string pathToFile)
    {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            string text = File.ReadAllText(pathToFile);

#if NET6_0_OR_GREATER
            string[] lines = text.Split(Environment.NewLine);
#else
            string[] lines = text.Replace(" ", String.Empty).Split(Environment.NewLine.ToCharArray());
#endif
            foreach (string line in lines)
            {
                string[] lineSplit = line.Split('=');
                
                list.Add(new KeyValuePair<string, string>(lineSplit[0], lineSplit[1]));
            }

            return list.ToArray();
    }

    /// <summary>
    /// Writes the specified data to a .txt Text file.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="pathToFile"></param>
    public void WriteToFile(IEnumerable<KeyValuePair<string, string>> data, string pathToFile)
    {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> pair in data)
            {
                stringBuilder.AppendLine($"{pair.Key}={pair.Value}");
            }
            
            File.WriteAllText(pathToFile, stringBuilder.ToString());
    }
}