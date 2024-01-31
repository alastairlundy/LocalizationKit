using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlastairLundy.LocalizationKit.Interfaces
{
    public interface ILocalizationFileProvider
    {
        KeyValuePair<string, string>[] Get(string pathToFile);

        void WriteToFile(KeyValuePair<string, string>[] data, string pathToFile);
    }
}
