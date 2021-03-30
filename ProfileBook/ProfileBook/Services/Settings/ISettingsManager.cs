using ProfileBook.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Settings
{
    public interface ISettingsManager
    {
        bool IsListUpdated { get; set; }

        ESortType SortType { get; set; }

        ETheme Theme { get; set; }

        ELanguage Language { get; set; }

        void UpdateTheme(ETheme theme);

        void ClearSettings();
    }
}
