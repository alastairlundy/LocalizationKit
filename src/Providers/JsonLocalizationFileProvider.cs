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

namespace LocalizationKit.Providers
{
    public class JsonLocalizationFileProvider : ILocalizationFileProvider
    {
        public KeyValuePair<string, string>[] Get(string pathToFile)
        {
            try
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

                string jsonText = File.ReadAllText(pathToFile);

                jsonText = jsonText.Replace("{", string.Empty);
                jsonText = jsonText.Replace("}", string.Empty);
                jsonText = jsonText.Replace(",", string.Empty);
                jsonText = jsonText.Replace('"', '0');
                jsonText = jsonText.Replace("0", string.Empty);
                jsonText = jsonText.Replace("'", string.Empty);

#if NET6_0_OR_GREATER
                string[] lines = jsonText.Split(Environment.NewLine);
#else
                string[] lines = jsonText.Replace(" ", String.Empty).Split(Environment.NewLine.ToCharArray());
#endif

                foreach (var line in lines)
                {
                    var newLine = line.Replace(" ", String.Empty);
                    var splitLine = newLine.Split(':');

                    if (splitLine.Length > 1)
                    {
                        list.Add(new KeyValuePair<string, string>(splitLine[0], splitLine[1]));
                    }
                }

                return list.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return list.ToArray();
        }

        public void WriteToFile(KeyValuePair<string, string>[] data, string pathToFile)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append('{');
            stringBuilder.AppendLine();
            
            foreach (var pair in data)
            {
                stringBuilder.Append('"');
                stringBuilder.Append(pair.Key);
                stringBuilder.Append('"');
                
                stringBuilder.Append(' ');
                stringBuilder.Append(':');
                stringBuilder.Append(' ');

                stringBuilder.Append('"');
                stringBuilder.Append(pair.Value);
                stringBuilder.Append('"');
                stringBuilder.Append(',');
                stringBuilder.AppendLine();
            }

            stringBuilder.Append('}');
            stringBuilder.AppendLine();
            
            File.WriteAllText(pathToFile, stringBuilder.ToString());
        }
    }
}
