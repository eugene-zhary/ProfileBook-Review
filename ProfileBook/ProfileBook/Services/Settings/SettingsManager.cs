using ProfileBook.Enums;
using ProfileBook.Localization;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProfileBook.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        #region --- Properties ---

        public ESortType SortType {
            get => (ESortType)Enum.Parse(typeof(ESortType), Preferences.Get(key: nameof(SortType), defaultValue: ESortType.Name.ToString()));
            set => Preferences.Set(nameof(SortType), value.ToString());
        }

        public ETheme Theme {
            get => (ETheme)Enum.Parse(typeof(ETheme), Preferences.Get(key: nameof(Theme), defaultValue: ETheme.Light.ToString()));
            set {
                Preferences.Set(nameof(Theme), value.ToString());
                UpdateTheme(value);
            }
        }

        public ELanguage Language {
            get => (ELanguage)Enum.Parse(typeof(ELanguage), Preferences.Get(key: nameof(Language), defaultValue: ELanguage.English.ToString()));
            set {
                Preferences.Set(nameof(Language), value.ToString());
                MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(value));
            }
        }

        public bool IsListUpdated {
            get => Preferences.Get(nameof(IsListUpdated), true);
            set => Preferences.Set(nameof(IsListUpdated), value);
        }

        #endregion

        public SettingsManager()
        {
            MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(Language));
            UpdateTheme(Theme);
        }


        public void UpdateTheme(ETheme theme)
        {
            switch (theme) {
                case ETheme.Dark:
                    SetColors("#d0d0d0", "#293241", "#3d5a80");
                    break;
                case ETheme.Light:
                default:
                    SetColors("#1d3557", "#ffffff", "#457b9d");
                    break;
            }
        }

        private void SetColors(string primary_color, string back_color, string front_color)
        {
            App.Current.Resources["PrimaryColor"] = Color.FromHex(primary_color);
            App.Current.Resources["BackColor"] = Color.FromHex(back_color);
            App.Current.Resources["FrontColor"] = Color.FromHex(front_color);
        }

        public void ClearSettings()
        {
            MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(ELanguage.English));
            UpdateTheme(ETheme.Light);
            Preferences.Clear();
        }
    }
}
