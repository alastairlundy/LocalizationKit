// See https://aka.ms/new-console-template for more information

using AlastairLundy.LocalizationKit;
using AlastairLundy.SettingsKit.Providers;

LocalizationManager localizationManager = new LocalizationManager();

string path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "testing" + Path.DirectorySeparatorChar +
              "en_us.json";

localizationManager.LoadLocalization("en_us", path, new JsonSettingsProvider<string, string>());

var localization = localizationManager.GetLocalization("en_us");

Console.WriteLine(localization.GetLocalizedPhrase("earth.flatness").Value);