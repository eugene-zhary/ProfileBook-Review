using ProfileBook.Enums;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using Xamarin.Forms;

namespace ProfileBook.Localization
{
    public class LocalizedResources : INotifyPropertyChanged
    {
        readonly ResourceManager ResourceManager;
        CultureInfo CurrentCultureInfo;

        public string this[string key] {
            get => ResourceManager.GetString(key, CurrentCultureInfo);
        }

        public LocalizedResources(Type resource, ELanguage language)
        {
            CurrentCultureInfo = GetCultureInfo(language);
            ResourceManager = new ResourceManager(resource);

            MessagingCenter.Subscribe<object, CultureChangedMessage>(this, string.Empty, OnCultureChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnCultureChanged(object s, CultureChangedMessage ccm)
        {
            CurrentCultureInfo = ccm.NewCultureInfo;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
        }

        /// <summary>
        /// get culture info with Language enum
        /// </summary>
        /// <param name="lang">Language enum</param>
        /// <returns>CultureInfo</returns>
        public static CultureInfo GetCultureInfo(ELanguage lang)
        {
            switch (lang) {
                case ELanguage.Russian:
                    return new CultureInfo("ru");
                case ELanguage.English:
                default:
                    return new CultureInfo("en");
            }
        }
    }
}
