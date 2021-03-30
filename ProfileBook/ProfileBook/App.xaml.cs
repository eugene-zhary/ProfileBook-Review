using Prism;
using Prism.Ioc;
using ProfileBook.Dialogs;
using ProfileBook.Repositories;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.ViewModels;
using ProfileBook.ViewModels.Dialogs;
using ProfileBook.Views;
using ProfileBook.Views.Main;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App
    {
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
           
            if (Preferences.Get("UserId", 0) == 0) {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInPage)}");
            }
            else {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainListPage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //services
            containerRegistry.Register(typeof(IRepository<>), typeof(Repository<>));
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IAuthorizationManager>(Container.Resolve<AuthorizationManager>());
            containerRegistry.RegisterInstance<IProfileManager>(Container.Resolve<ProfileManager>());

            //dialogs
            containerRegistry.RegisterDialog<ShowImageDialog, ShowImageDialogViewModel>();
            containerRegistry.RegisterDialog<PickImageDialog, PickImageDialogViewModel>();

            //navigations
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListPage, MainListPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPage, AddEditPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }
    }
}
