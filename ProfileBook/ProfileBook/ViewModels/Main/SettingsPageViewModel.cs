using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Localization;
using ProfileBook.Services;
using ProfileBook.Services.Settings;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private ISettingsService settingsService;

        #region sort

        private bool isDateChecked;
        public bool IsDateChecked {
            get => isDateChecked;
            set {
                SetProperty(ref isDateChecked, value, nameof(IsDateChecked));
                if (value) {
                    updateSort(SortType.Date);
                }
            }
        }

        private bool isNameChecked;
        public bool IsNameChecked {
            get => isNameChecked;
            set {
                SetProperty(ref isNameChecked, value, nameof(IsNameChecked));
                if (value) {
                    updateSort(SortType.Name);
                }
            }
        }

        private bool isNickNameChecked;
        public bool IsNickNameChecked {
            get => isNickNameChecked;
            set {
                SetProperty(ref isNickNameChecked, value, nameof(IsNickNameChecked));
                if (value) {
                    updateSort(SortType.NickName);
                }
            }
        }

        #endregion

        #region themes

        private bool isDark;
        public bool IsDark {
            get => isDark;
            set {
                SetProperty(ref isDark, value, nameof(IsDark));

                // set or update current theme
                if (value)
                    ThemeManager.UpdateTheme(Theme.Dark);
                else
                    ThemeManager.UpdateTheme(Theme.Light);
            }
        }

        #endregion

        #region lang

        private bool isEnglishChecked;
        public bool IsEnglishChecked {
            get => isEnglishChecked;
            set {
                SetProperty(ref isEnglishChecked, value, nameof(IsEnglishChecked));
                if (value) {
                    updateLang(Language.English);
                }
            }
        }

        private bool isRussianChecked;
        public bool IsRussianChecked {
            get => isRussianChecked;
            set {
                SetProperty(ref isRussianChecked, value, nameof(IsRussianChecked));
                if (value) {
                    updateLang(Language.Russian);
                }
            }
        }

        #endregion

        public SettingsPageViewModel(INavigationService navigationService, ISettingsService settingsService) : base(navigationService)
        {
            setSortCheck();
            setThemeCheck();
            setLangCheck();
            this.settingsService = settingsService;
        }


        public override async void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            // update settings in the database
            await settingsService.UpdateSettings(App.CurrentSettings);
        }


        /// <summary>
        /// update the local sort settings
        /// </summary>
        /// <param name="sort">SortType</param>
        private void updateSort(SortType sort)
        {
            if (App.CurrentSettings.Sort != sort) {
                App.CurrentSettings.Sort = sort;
                App.UpdateList = true;
            }
        }

        /// <summary>
        /// update the local language settings
        /// </summary>
        /// <param name="language">Language</param>
        private void updateLang(Language language)
        {
            App.CurrentSettings.Language = language;
            MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(language));
        }



        /// <summary>
        /// set the current theme checked
        /// </summary>
        private void setThemeCheck()
        {
            switch (App.CurrentSettings.Theme) {
                case Theme.Light:
                default:
                    isDark = false;
                    break;
                case Theme.Dark:
                    isDark = true;
                    break;
            }
        }

        /// <summary>
        /// set the current sort type checked
        /// </summary>
        private void setSortCheck()
        {
            switch (App.CurrentSettings.Sort) {
                case SortType.Date:
                default:
                    isDateChecked = true;
                    break;
                case SortType.Name:
                    isNameChecked = true;
                    break;
                case SortType.NickName:
                    isNickNameChecked = true;
                    break;
            }
        }

        /// <summary>
        /// set the current lang checked
        /// </summary>
        private void setLangCheck()
        {
            switch (App.CurrentSettings.Language) {
                case Language.English:
                default:
                    isEnglishChecked = true;
                    break;
                case Language.Russian:
                    isRussianChecked = true;
                    break;
            }
        }
    }
}
