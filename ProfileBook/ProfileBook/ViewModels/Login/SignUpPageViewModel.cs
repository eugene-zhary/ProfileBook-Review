using Prism.Commands;
using Prism.Navigation;
using System;
using ProfileBook.Services.Authorization;
using System.ComponentModel;
using Xamarin.Forms;
using ProfileBook.Views;
using Prism.Services;

namespace ProfileBook.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
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
        private string _userConfirmPassword;
        public string UserConfirmPassword {
            get => _userConfirmPassword;
            set => SetProperty(ref _userConfirmPassword, value, nameof(UserConfirmPassword));
        }

        private bool _isValid;
        public bool IsValid {
            get => _isValid;
            set => SetProperty(ref _isValid, value, nameof(IsValid));
        }

        #endregion

        public SignUpPageViewModel(INavigationService navigationService, IAuthorizationManager authorizationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            this._authorizationService = authorizationService;
            this._pageDialogService = pageDialogService;

            this._userLogin = String.Empty;
            this._userPassword = String.Empty;
            this._userConfirmPassword = String.Empty;

            this.SignUpCommand = new DelegateCommand(executeSignUp).ObservesCanExecute(() => IsValid);
        }

        #region --- Commands ---

        public DelegateCommand SignUpCommand { get; set; }

        private async void executeSignUp()
        {
            string hints = Validators.ValidationHints.GetSignUpHints(UserLogin, UserPassword, UserConfirmPassword);

            if (hints.Length > 0) {
                await this._pageDialogService.DisplayAlertAsync("SignUp", hints, "OK");
                return;
            }

            if (await _authorizationService.RegUser(this.UserLogin, this.UserPassword, this.UserConfirmPassword)) {
                var nav_params = new NavigationParameters {
                    { "Login", this.UserLogin }
                };
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}", nav_params);
            }
            else {
                await this._pageDialogService.DisplayAlertAsync("SignUp", "Login already exists", "OK");
            }
        }

        #endregion

        #region --- Overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName) {
                case nameof(UserLogin):
                case nameof(UserPassword):
                case nameof(UserConfirmPassword):

                    this.IsValid = UserPassword != null &&
                          UserPassword.Length != 0 &&
                          UserLogin != null &&
                          UserLogin.Length != 0 &&
                          UserConfirmPassword != null &&
                          UserConfirmPassword.Length != 0;

                    break;
            }
        }

        #endregion
    }
}
