using System;
using System.Collections.Generic;
using LocalizationKit.Interfaces;

namespace LocalizationKit.Providers
{
    public class JsonLocalizationFileProvider : ILocalizationFileProvider
    {
        public KeyValuePair<string, string>[] Get(string pathToFile)
        {
            throw new NotImplementedException();
        }

        public void WriteToFile(KeyValuePair<string, string>[] data, string pathToFile)
        {
            throw new NotImplementedException();
        }
    }
}
