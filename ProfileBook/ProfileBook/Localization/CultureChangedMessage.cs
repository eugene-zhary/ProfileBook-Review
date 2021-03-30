using ProfileBook.Enums;
using System.Globalization;

namespace ProfileBook.Localization
{
    public class CultureChangedMessage
    {
        public CultureInfo NewCultureInfo { get; private set; }

        public CultureChangedMessage(ELanguage lang)
        {
            NewCultureInfo = LocalizedResources.GetCultureInfo(lang);
        }
    }
}
