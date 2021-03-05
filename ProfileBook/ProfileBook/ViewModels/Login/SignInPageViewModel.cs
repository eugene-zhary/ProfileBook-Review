using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Services.Authorization;
using ProfileBook.Views;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly IAuthorizationManager _authorizationService;
        private readonly IPageDialogService _pageDialogService;

        #region --- Properties ---

        private string _userLogin;
        public string UserLogin {
            get => _userLogin;
            set => SetProperty(ref _userLogin, value, nameof(UserLogin));

        }

        private string _userPassword;
        public string UserPassword {
            get => _userPassword;
            set => SetProperty(ref _userPassword, value, nameof(UserPassword));
        }

        private bool _isValid;
        public bool IsValid {
            get => _isValid;
            set => SetProperty(ref _isValid, value, nameof(IsValid));
        }

        #endregion

        public SignInPageViewModel(INavigationService navigationService, IAuthorizationManager authorizationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            this._pageDialogService = pageDialogService;
            this._authorizationService = authorizationService;

            this._userLogin = String.Empty;
            this._userPassword = String.Empty;

            this.SignInCommand = new DelegateCommand(executeSignIn).ObservesCanExecute(() => IsValid);
        }

        #region --- Commands ---

        public DelegateCommand SignInCommand { get; set; }

        private async void executeSignIn()
        {
            if (await this._authorizationService.SignIn(this.UserLogin, this.UserPassword)) {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListPage)}");
            }
            else {
                await _pageDialogService.DisplayAlertAsync("SignIn", "Invalid login or password!", "OK");
            }
        }

        public ICommand SignUpCommand => new Command(async () => {
            await NavigationService.NavigateAsync(nameof(SignUpPage));
        });

        #endregion

        #region --- Overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(UserLogin) || args.PropertyName == nameof(UserPassword)) {

                this.IsValid = UserPassword != null &&
                           UserPassword.Length != 0 &&
                           _userLogin != null &&
                           UserLogin.Length != 0;
            }
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            this.UserLogin = parameters.GetValue<string>("Login");
        }

        #endregion
    }
}
