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
using System.Collections;
using System.Collections.Generic;

using System.Reflection;
using System.Resources;
using LocalizationKit.Interfaces;

namespace LocalizationKit.Providers;

/// <summary>
/// A class to read and write Localizations to/from Resource files.
/// </summary>
public class ResourceLocalizationFileProvider : ILocalizationFileProvider
{
    /// <summary>
    /// Retrieves string Keys and Values stored in a Resource File.
    /// </summary>
    /// <param name="pathToFile"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public IEnumerable<KeyValuePair<string, string>> Get(string pathToFile)
    {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            string baseName = pathToFile;

            if (pathToFile.EndsWith(".resw"))
            {
                baseName = pathToFile.Replace(".resw", string.Empty);
            }
            if (pathToFile.EndsWith(".resx"))
            {
                baseName = pathToFile.Replace(".resx", string.Empty);
            }
            if (pathToFile.EndsWith(".resources"))
            {
                baseName = pathToFile.Replace(".resources", string.Empty);
            }
        
            ResourceManager resourceManager = new ResourceManager(baseName, Assembly.GetEntryAssembly() ?? throw new NullReferenceException("Entry Assembly was null."));
            
            ResourceReader reader = new ResourceReader(resourceManager.GetStream(pathToFile) ?? throw new NullReferenceException());

            IDictionaryEnumerator readerEnumerator = reader.GetEnumerator();
            using var readerEnumerator1 = readerEnumerator as IDisposable;

            while (readerEnumerator.MoveNext())
            {
                list.Add(new KeyValuePair<string, string>((string)readerEnumerator.Key,
                    (string)readerEnumerator
                        .Value));
            }
            
            readerEnumerator.Reset();
            reader.Dispose();
            reader.Close();

            return list.ToArray();
    }

    /// <summary>
    /// Writes the specified data to a Resource file.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="pathToFile"></param>
    public void WriteToFile(IEnumerable<KeyValuePair<string, string>> data, string pathToFile)
    {
            ResourceWriter resourceWriter = new ResourceWriter(pathToFile);

            foreach (KeyValuePair<string, string> pair in data)
            {
                resourceWriter.AddResource(pair.Key, pair.Value);
            }
            
            resourceWriter.Generate();
            
            resourceWriter.Close();
    }
}