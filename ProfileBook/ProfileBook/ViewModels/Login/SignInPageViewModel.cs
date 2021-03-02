using Prism.Commands;
using Prism.Navigation;
using ProfileBook.Services.Authentication;
using ProfileBook.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly IAuthenticationService authenticationService;

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

        private bool isValid;
        public bool IsValid {
            get { return isValid; }
            set { SetProperty(ref isValid, value, nameof(IsValid)); }
        }

        private void RaiseValidationChanded()
        {
            this.IsValid = UserPassword != null &&
                           UserPassword.Length != 0 && 
                           userLogin != null &&
                           UserLogin.Length != 0;
        }

        public SignInPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService) : base(navigationService)
        {
            this.userLogin = String.Empty;
            this.userPassword = String.Empty;

            this.SignInCommand = new DelegateCommand(executeSignIn).ObservesCanExecute(() => IsValid);
            this.SignUpCommand = new Command(executeSignUp);

            this.authenticationService = authenticationService;
        }

        public DelegateCommand SignInCommand { get; set; }

        /// <summary>
        /// navigate to main list
        /// </summary>
        private async void executeSignIn()
        {
            if (await this.authenticationService.SignIn(this.UserLogin, this.UserPassword)) {
                App.UpdateList = true;
                await NavigationService.NavigateAsync($"/NavigationPage/{nameof(MainListPage)}");
            }
        }

        public ICommand SignUpCommand { get; set; }

        /// <summary>
        /// navigate to sign up
        /// </summary>
        private async void executeSignUp()
        {
            await NavigationService.NavigateAsync(nameof(SignUpPage));
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            this.UserLogin = parameters.GetValue<string>("Login");
        }
    }
}
