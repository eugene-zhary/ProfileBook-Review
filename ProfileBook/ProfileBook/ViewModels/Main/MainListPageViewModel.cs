using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services;
using ProfileBook.Services.Main;
using ProfileBook.Views;
using ProfileBook.Views.Main;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Localization;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        private readonly IMainService mainService;

        public ObservableCollection<Profile> ProfileList { get; set; }

        private bool isEmpty;
        public bool IsEmpty {
            get => isEmpty;
            set => SetProperty(ref isEmpty, value, nameof(IsEmpty));
        }

        public MainListPageViewModel(INavigationService navigationService, IMainService mainService) : base(navigationService)
        {
            App.CurrentSettings = LocalService.ReadSettings();

            this.mainService = mainService;
            this.IsEmpty = true;
            this.ProfileList = new ObservableCollection<Profile>();

            this.LogOutCommand = new Command(executeLogOut);
            this.AddProfileCommand = new Command(executeAddProfile);
            this.SettingsCommand = new Command(executeSettingsCommand);
        }


        // fill the profile list from database
        private async Task updateProfileList()
        {
            this.ProfileList.Clear();
            var profiles = await mainService.GetProfiles(App.CurrentUser.Id);

            if (profiles != null) {
                profiles.ToList().ForEach(item => {
                    //Add all events
                    item.RemoveProfile += Item_RemoveProfile;
                    item.EditProfile += Item_EditProfile;
                    item.ShowImage += Item_ShowImage;
                    this.ProfileList.Add(item);
                });
            }

            UpdateLabel();
        }

        #region commands

        public ICommand LogOutCommand { get; private set; }

        /// <summary>
        /// log out from current profile
        /// </summary>
        private async void executeLogOut()
        {
            ThemeManager.UpdateTheme(Enums.Theme.Light);
            LocalService.Delete();
            await NavigationService.NavigateAsync($"/NavigationPage/{nameof(SignInPage)}");
        }

        public ICommand AddProfileCommand { get; private set; }

        /// <summary>
        /// add new profile
        /// </summary>
        private async void executeAddProfile()
        {
            await NavigationService.NavigateAsync(nameof(AddEditPage));
        }
        
        public ICommand SettingsCommand { get; private set; }

        /// <summary>
        /// navigate to the settings
        /// </summary>
        private async void executeSettingsCommand()
        {
            await NavigationService.NavigateAsync(nameof(SettingsPage));
        }

        #endregion

        #region profile list events

        // show selected profile image
        private async void Item_ShowImage(object sender, System.EventArgs e)
        {
            if (sender is Profile profile) {
                await this.mainService.ShowImage(profile.Image);
            }
        }

        // edit selected profile
        private async void Item_EditProfile(object sender, System.EventArgs e)
        {
            if (sender is Profile profile) {
                var nav_params = new NavigationParameters {
                    { "Profile", profile }
                };
                await NavigationService.NavigateAsync("AddEditPage", nav_params);
            }
        }

        // remove selected profile
        private async void Item_RemoveProfile(object sender, System.EventArgs e)
        {
            if (sender is Profile profile && await mainService.RemoveProfile(profile)) {
                //remove all events
                profile.EditProfile -= Item_EditProfile;
                profile.RemoveProfile -= Item_RemoveProfile;
                profile.ShowImage -= Item_ShowImage;
                this.ProfileList.Remove(profile);
                UpdateLabel();
            }
        }

        #endregion

        /// <summary>
        /// enable label if the profile list is empty
        /// </summary>
        private void UpdateLabel()
        {
            IsEmpty = (ProfileList.Count <= 0);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (App.UpdateList) {
                await updateProfileList();
                App.UpdateList = false;
            }

            // set or update all settings
            ThemeManager.UpdateTheme(App.CurrentSettings.Theme);
            MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(App.CurrentSettings.Language));
        }
    }
}
