# LocalizationKit

LocalizationKit is a library designed to make managing localizations in .NET easier.


## Licensing

LocalizationKit, which is MIT licensed, relies on [SettingsKit](https://gitlab.com/aluminiumtechdevkit/SettingsKit) to manage Localizations stored in files and allow developers to get localized strings for use in their applications.

SettingsKit is also MIT licensed and is also made by me.

## Differences between LocalizationKit and SettingsKit

The main differences between LocalizationKit and SettingsKit, besides SettingsKit being meant for generic Settings management, is that Localizationkit is not designed to allow creating localizations. It's primarily intended to load existing localization files and provide programmatic access to the localizations. 

In the future I _may_ create a tool to generate localization files for use with LocalizationKit and/or SettingsKit.

## Roadmap
As this project relies on SettingsKit, the roadmap of LocalizationKit will more or less track [that of SettingsKit](https://gitlab.com/aluminiumtechdevkit/SettingsKit/-/blob/master/Roadmap.md) with some differences for localization specific things.
