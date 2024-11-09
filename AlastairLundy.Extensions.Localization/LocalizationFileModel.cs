
using AlastairLundy.Extensions.Localization.Enums;

namespace AlastairLundy.Extensions.Localization
{
    public class LocalizationFileModel
    {
        public LocalizationFileModel(string filePath, LocalizationFileType fileType, LocaleModel locale)
        {
            FilePath = filePath;
            Locale = locale;
            FileType = fileType;
        }
        
        
        public LocaleModel Locale { get; set; } 
#if NET6_0_OR_GREATER
        public string? FileName { get; set; }
#elif NETSTANDARD2_0
        public string FileName { get; set; }
#endif
        public string FilePath { get; set; }

        public LocalizationFileType FileType { get; set; }
    }
}