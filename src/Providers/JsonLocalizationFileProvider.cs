using AlastairLundy.LocalizationKit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlastairLundy.LocalizationKit.Providers
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
