using Prism.Navigation;
using ProfileBook.Localization;
using ProfileBook.Models;
using ProfileBook.Services.Profile;
using ProfileBook.Validators;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Prism.Services;
using ProfileBook.Views;
using Prism.Services.Dialogs;
using ProfileBook.Dialogs;

namespace ProfileBook.ViewModels
{
    public class AddEditPageViewModel : ViewModelBase, INavigatedAware
    {
        public Profile CurrentProfile { get; set; }

        private readonly IProfileManager _profileManager;
        private readonly IPageDialogService _pageDialogService;
        private readonly IDialogService _dialogService;

        public AddEditPageViewModel(IProfileManager profileManager, 
            INavigationService navigationService, 
            IPageDialogService pageDialogService, 
            IDialogService dialogService) : base(navigationService)
        {
            this._profileManager = profileManager;
            this._pageDialogService = pageDialogService;
            this._dialogService = dialogService;

            CurrentProfile = new Profile();
        }

        #region --- Commands ---

        public ICommand SaveProfileCommand => new Command(async () => {

            string hints = ValidationHints.GetProfileHints(CurrentProfile, Resources["AddEditValidateName"], Resources["AddEditValidateNickName"]);

            if (!hints.Equals(String.Empty)) {
                await _pageDialogService.DisplayAlertAsync(Resources["AddEditAlertTitle"], hints, Resources["Confirm"]);
                return;
            }

            await _profileManager.SaveProfile(CurrentProfile);
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListPage)}");
        });

        public ICommand OpenImageDialogCommand => new Command(async () => {

            IDialogResult result = await _dialogService.ShowDialogAsync(nameof(PickImageDialog));
            string img_path = result.Parameters.GetValue<string>("ImagePath");

            if (img_path != null) {
                CurrentProfile.Image = img_path;
                RaisePropertyChanged(nameof(CurrentProfile));
                await _profileManager.SaveProfile(CurrentProfile);
            }
        });

        #endregion

        #region --- Overrides ---

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters["Profile"] is Profile profile) {
                this.CurrentProfile = profile;
                RaisePropertyChanged(nameof(CurrentProfile));
            }
        }

        #endregion
    }
}
