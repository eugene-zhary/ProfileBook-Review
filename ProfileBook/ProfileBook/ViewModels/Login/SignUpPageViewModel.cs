using Prism.Commands;
using Prism.Navigation;
using System;
using ProfileBook.Services.Authorization;

namespace ProfileBook.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        private readonly IAuthorizationService authorizationService;

        private string userLogin;
        public string UserLogin {
            get => userLogin;
            set {
                SetProperty(ref userLogin, value, nameof(UserLogin));
                RaiseValidationChanded();
            }
        }
        private string userPassword;
        public string UserPassword {
            get => userPassword;
            set {
                SetProperty(ref userPassword, value, nameof(UserPassword));
                RaiseValidationChanded();
            }
        }
        private string userConfirmPassword;
        public string UserConfirmPassword {
            get => userConfirmPassword;
            set {
                SetProperty(ref userConfirmPassword, value, nameof(UserConfirmPassword));
                RaiseValidationChanded();
            }
        }

        private bool isValid;
        public bool IsValid {
            get { return isValid; }
            set { SetProperty(ref isValid, value, nameof(IsValid)); }
        }

        private void RaiseValidationChanded()
        {
            this.IsValid = UserPassword != null &&
                           UserPassword.Length != 0 && 
                           UserLogin != null &&
                           UserLogin.Length != 0 && 
                           UserConfirmPassword != null &&
                           UserConfirmPassword.Length != 0;
        }

        public SignUpPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService) : base(navigationService)
        {
            this.authorizationService = authorizationService;

            this.userLogin = String.Empty;
            this.userPassword = String.Empty;
            this.userConfirmPassword = String.Empty;

            this.SignUpCommand = new DelegateCommand(executeSignUp).ObservesCanExecute(() => IsValid);
        }

        public DelegateCommand SignUpCommand { get; set; }

        /// <summary>
        /// authorizate new account, then navigate to the sign in with a new login
        /// </summary>
        private async void executeSignUp()
        {
            if (await authorizationService.RegUser(this.UserLogin, this.UserPassword, this.UserConfirmPassword)) {
                var nav_params = new NavigationParameters();
                nav_params.Add("Login", this.UserLogin);
                await NavigationService.NavigateAsync("/NavigationPage/SignInPage", nav_params);
            }
        }
    }
}
