using ProfileBook.Enums;
using Xamarin.Forms;

namespace ProfileBook.Services
{
    public static class ThemeManager
    {
        /// <summary>
        /// change theme
        /// </summary>
        /// <param name="theme">Theme enum</param>
        public static void UpdateTheme(Theme theme)
        {
            if(App.CurrentSettings.Theme != theme)
                App.CurrentSettings.Theme = theme;

            switch (theme) {
                case Theme.Dark:
                    SetColors("#ffffff", "#293241", "#3d5a80");
                    break;
                case Theme.Light:
                default:
                    SetColors("#1d3557", "#ffffff", "#457b9d");
                    break;
            }
        }

        // set the colors for resources
        private static void SetColors(string primary_color, string back_color, string front_color)
        {
            App.Current.Resources["PrimaryColor"] = Color.FromHex(primary_color);
            App.Current.Resources["BackColor"] = Color.FromHex(back_color);
            App.Current.Resources["FrontColor"] = Color.FromHex(front_color);
        }
    }
}
