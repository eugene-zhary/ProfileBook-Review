using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using ProfileBook.Dialogs;
using ProfileBook.Models;
using ProfileBook.Properties;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Settings;
using ProfileBook.Views;
using ProfileBook.Views.Main;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        private readonly IProfileManager _profileManager;
        private readonly ISettingsManager _settingsManager;
        private readonly IDialogService _dialogService;
        private readonly IPageDialogService _pageDialogService;

        #region --- Properties ---

        public ObservableCollection<Profile> ProfileList { get; set; }

        private bool _isEmpty;
        public bool IsEmpty {
            get => _isEmpty;
            set => SetProperty(ref _isEmpty, value, nameof(IsEmpty));
        }

        #endregion

        public MainListPageViewModel(INavigationService navigationService,
            IProfileManager profileManager,
            ISettingsManager settingsManager,
            IDialogService dialogService,
            IPageDialogService pageDialogService) : base(navigationService)
        {
            this._profileManager = profileManager;
            this._settingsManager = settingsManager;
            this._dialogService = dialogService;
            this._pageDialogService = pageDialogService;

            this.ProfileList = new ObservableCollection<Profile>();
            this.IsEmpty = true;
        }

        #region --- Comands ---

        public ICommand RemoveProfileCommand => new Command(async (sender) => {

            bool isConfirmed = await _pageDialogService.DisplayAlertAsync(AppResources.MainDelete, AppResources.MainDeleteConfirm, AppResources.Accept, AppResources.Denie);

            if (sender is Profile profile && isConfirmed) {
                await _profileManager.RemoveProfile(profile);
                this.ProfileList.Remove(profile);
            }
        });

        public ICommand EditProfileCommand => new Command(async (sender) => {
            if (sender is Profile profile) {
                var nav_params = new NavigationParameters {
                    { "Profile", profile }
                };
                await NavigationService.NavigateAsync($"{nameof(AddEditPage)}", nav_params);
            }
        });

        public ICommand ShowImageCommand => new Command(async (sender) => {
            if (sender is Profile profile) {
                var dialog_params = new DialogParameters {
                    {"Image", ImageSource.FromFile(profile.Image) }
                };
                await _dialogService.ShowDialogAsync(nameof(ShowImageDialog), dialog_params);
            }
        });

        public ICommand AddProfileCommand => new Command(async () => {
            await NavigationService.NavigateAsync(nameof(AddEditPage));
        });

        public ICommand SettingsCommand => new Command(async () => {
            await NavigationService.NavigateAsync(nameof(SettingsPage));
        });

        public ICommand LogOutCommand => new Command(async () => {
            _settingsManager.ClearSettings();
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
        });

        #endregion

        #region --- Overrides ---

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (_settingsManager.IsListUpdated) {
                await updateProfileList();
                _settingsManager.IsListUpdated = false;
            }
        }

        #endregion

        #region --- Helpers ---

        private async Task updateProfileList()
        {
            this.ProfileList.Clear();
            IsEmpty = true;

            var profiles = await _profileManager.GetProfiles(Preferences.Get("UserId", 0));

            if (profiles != null && profiles.Count() > 0) {
                profiles.ToList().ForEach(item => this.ProfileList.Add(item));
                IsEmpty = false;
            }
        }

        #endregion
    }
}
