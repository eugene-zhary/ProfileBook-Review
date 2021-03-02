using Prism;
using Prism.Ioc;
using Prism.Navigation;
using ProfileBook.Dialogs;
using ProfileBook.Enums;
using ProfileBook.Models;
using ProfileBook.Repositories;
using ProfileBook.Services;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Main;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.ViewModels;
using ProfileBook.ViewModels.Dialogs;
using ProfileBook.Views;
using ProfileBook.Views.Main;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App
    {
        /// <summary>
        /// local user
        /// </summary>
        public static User CurrentUser { get; set; }
        /// <summary>
        /// local settings
        /// </summary>
        public static Settings CurrentSettings { get; set; }
        /// <summary>
        /// update list in the main page after navigation
        /// </summary>
        public static bool UpdateList { get; set; }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            CurrentUser = LocalService.ReadUser();

            if (CurrentUser == null) {
                await NavigationService.NavigateAsync("NavigationPage/SignInPage");
            }
            else {
                UpdateList = true;
                await NavigationService.NavigateAsync("NavigationPage/MainListPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            //services
            containerRegistry.Register(typeof(IRepository<>), typeof(Repository<>));
            containerRegistry.Register<IAuthenticationService, AuthenticationService>();
            containerRegistry.Register<IAuthorizationService, AuthorizationService>();
            containerRegistry.Register<IMainService, MainService>();
            containerRegistry.Register<IProfileService, ProfileService>();
            containerRegistry.Register<ISettingsService, SettingsService>();

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
