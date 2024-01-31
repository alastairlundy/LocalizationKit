using AlastairLundy.SettingsKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlastairLundy.LocalizationKit.Interfaces
{
    public interface ILocalizationFile
    {
        public ILocalizationFileProvider SettingsProvider { get; }

        public void Add(KeyValuePair<string, string> pair);

        public void Remove(KeyValuePair<string, string> pair);

        public KeyValuePair<string, string>[] Get();
        public string Get(string key);

        internal void SaveFile();
    }
}
