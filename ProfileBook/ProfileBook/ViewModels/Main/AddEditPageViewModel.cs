using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Profile;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class AddEditPageViewModel : ViewModelBase, INavigatedAware
    {
        public Profile CurrentProfile { get; set; }
        private IProfileService profileService;

        public AddEditPageViewModel(IProfileService profileService, INavigationService navigationService) : base(navigationService)
        {
            this.profileService = profileService;
            CurrentProfile = new Profile();

            SaveProfileCommand = new Command(executeSaveProfile);
            OpenImageDialogCommand = new Command(executeImageDialog);
        }


        public ICommand SaveProfileCommand { get; private set; }

        /// <summary>
        /// save current profile
        /// </summary>
        private async void executeSaveProfile()
        {
            if (await profileService.SaveProfile(CurrentProfile)) {
                App.UpdateList = true;
                await NavigationService.NavigateAsync("/NavigationPage/MainListPage");
            }
        }

        public ICommand OpenImageDialogCommand { get; private set; }

        /// <summary>
        /// set the new image
        /// </summary>
        private async void executeImageDialog()
        {
            string img_path = await profileService.GetImagePath(CurrentProfile);
            if (img_path != null) {
                CurrentProfile.Image = img_path;
                RaisePropertyChanged(nameof(CurrentProfile));
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters["Profile"] is Profile profile) {
                this.CurrentProfile = profile;
                RaisePropertyChanged(nameof(CurrentProfile));
            }
        }
    }
}
