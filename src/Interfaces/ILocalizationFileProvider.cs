using System.Collections.Generic;

namespace LocalizationKit.Interfaces
{
    public interface ILocalizationFileProvider
    {
        KeyValuePair<string, string>[] Get(string pathToFile);

        void WriteToFile(KeyValuePair<string, string>[] data, string pathToFile);
    }
}
