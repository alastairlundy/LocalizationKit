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

using System.Reflection;
using System.Resources;
using System.Threading;
using LocalizationKit.Interfaces;

namespace LocalizationKit.Providers;

public class ResourceLocalizationFileProvider : ILocalizationFileProvider
{
    public KeyValuePair<string, string>[] Get(string pathToFile)
    {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            string baseName = pathToFile;

            if (pathToFile.EndsWith(".resw"))
            {
                baseName = pathToFile.Replace(".resw", String.Empty);
            }
            if (pathToFile.EndsWith(".resx"))
            {
                baseName = pathToFile.Replace(".resx", String.Empty);
            }
            if (pathToFile.EndsWith(".resources"))
            {
                baseName = pathToFile.Replace(".resources", String.Empty);
            }
        
            ResourceManager resourceManager = new ResourceManager(baseName, Assembly.GetEntryAssembly() ?? throw new NullReferenceException("Entry Assembly was null."));

//            ResourceSet set = resourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, false, true);

            ResourceReader reader = new ResourceReader(resourceManager.GetStream(pathToFile) ?? throw new NullReferenceException());
            
            while (reader.GetEnumerator().MoveNext())
            {
                list.Add(new KeyValuePair<string, string>((string)reader.GetEnumerator()
                        .Key,
                    (string)reader.GetEnumerator()
                        .Value));
            }
            
            reader.Dispose();
            reader.Close();

            return list.ToArray();
    }

    public void WriteToFile(KeyValuePair<string, string>[] data, string pathToFile)
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