using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Localization;
using ProfileBook.Properties;
using System;
using Xamarin.Essentials;

namespace ProfileBook.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        public LocalizedResources Resources { get; private set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

            var lang_type = (ELanguage)Enum.Parse(typeof(ELanguage), Preferences.Get("Language", ELanguage.English.ToString()));
            Resources = new LocalizedResources(typeof(AppResources), lang_type);
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
