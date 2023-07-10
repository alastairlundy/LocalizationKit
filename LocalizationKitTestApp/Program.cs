// See https://aka.ms/new-console-template for more information

using AlastairLundy.LocalizationKit;
using AlastairLundy.SettingsKit;

LocalizationManager localizationManager = new LocalizationManager();

localizationManager.LoadLocalization("en_us", Environment.CurrentDirectory + Path.DirectorySeparatorChar + "locales" + Path.DirectorySeparatorChar + "en_us.json", new JsonSettingsProvider<string, string>());

var localization = localizationManager.GetLocalization("en_us");

Console.WriteLine(localization.GetLocalizedPhrase("earth.flatness").Value);