using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Services.Settings;
using System.ComponentModel;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private ISettingsManager _settingsManager;

        #region --- Properties ---

        private bool _isDateChecked;
        public bool IsDateChecked {
            get => _isDateChecked;
            set => SetProperty(ref _isDateChecked, value, nameof(IsDateChecked));
        }

        private bool _isNameChecked;
        public bool IsNameChecked {
            get => _isNameChecked;
            set => SetProperty(ref _isNameChecked, value, nameof(IsNameChecked));
        }

        private bool _isNickNameChecked;
        public bool IsNickNameChecked {
            get => _isNickNameChecked;
            set => SetProperty(ref _isNickNameChecked, value, nameof(IsNickNameChecked));
        }

        private bool _isDark;
        public bool IsDark {
            get => _isDark;
            set => SetProperty(ref _isDark, value, nameof(IsDark));
        }

        private bool _isEnglishChecked;
        public bool IsEnglishChecked {
            get => _isEnglishChecked;
            set => SetProperty(ref _isEnglishChecked, value, nameof(IsEnglishChecked));
        }

        private bool _isRussianChecked;
        public bool IsRussianChecked {
            get => _isRussianChecked;
            set => SetProperty(ref _isRussianChecked, value, nameof(IsRussianChecked));
        }

        #endregion

        public SettingsPageViewModel(INavigationService navigationService, ISettingsManager settingsService) : base(navigationService)
        {
            this._settingsManager = settingsService;

            setSortCheck();
            setThemeCheck();
            setLangCheck();
        }

        #region --- Overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(IsDateChecked) && IsDateChecked) {
                _settingsManager.SortType = ESortType.CreationDate;
                _settingsManager.IsListUpdated = true;
            }
            else if (args.PropertyName == nameof(IsNameChecked) && IsNameChecked) {
                _settingsManager.SortType = ESortType.Name;
                _settingsManager.IsListUpdated = true;
            }
            else if (args.PropertyName == nameof(IsNickNameChecked) && IsNickNameChecked) {
                _settingsManager.SortType = ESortType.NickName;
                _settingsManager.IsListUpdated = true;
            }
            else if (args.PropertyName == nameof(IsDark)) {
                _settingsManager.Theme = (IsDark) ? ETheme.Dark : ETheme.Light;
            }
            else if (args.PropertyName == nameof(IsEnglishChecked) && IsEnglishChecked) {
                _settingsManager.Language = ELanguage.English;
            }
            else if (args.PropertyName == nameof(IsRussianChecked) && IsRussianChecked) {
                _settingsManager.Language = ELanguage.Russian;
            }
        }

        #endregion

        #region --- Helpers ---

        private void setThemeCheck()
        {
            switch (_settingsManager.Theme) {
                case ETheme.Light:
                default:
                    _isDark = false;
                    break;
                case ETheme.Dark:
                    _isDark = true;
                    break;
            }
        }

        private void setSortCheck()
        {
            switch (_settingsManager.SortType) {
                case ESortType.CreationDate
:
                default:
                    _isDateChecked = true;
                    break;
                case ESortType.Name:
                    _isNameChecked = true;
                    break;
                case ESortType.NickName:
                    _isNickNameChecked = true;
                    break;
            }
        }

        private void setLangCheck()
        {
            switch (_settingsManager.Language) {
                case ELanguage.English:
                default:
                    _isEnglishChecked = true;
                    break;
                case ELanguage.Russian:
                    _isRussianChecked = true;
                    break;
            }
        }

        #endregion
    }
}
