// See https://aka.ms/new-console-template for more information

using AlastairLundy.LocalizationKit;

LocalizationManager localizationManager = new LocalizationManager();

localizationManager.LoadLocalization("en_us", Environment.CurrentDirectory + Path.DirectorySeparatorChar + "locales" + Path.DirectorySeparatorChar + "en_us.json");

var localization = localizationManager.GetLocalization("en_us");

Console.WriteLine(localization.Translations.Get("chocolate").Value);