using System;

namespace LocalizationKit.Exceptions;

public class LocaleNotFoundException : Exception
{
    public LocaleNotFoundException() : base("Locale not found: Either the locale has been spelled incorrectly or the locale does not exist.")
    {
        
    }
}