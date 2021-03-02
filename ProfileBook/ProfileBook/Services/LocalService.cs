using ProfileBook.Enums;
using ProfileBook.Models;
using System;
using System.IO;

namespace ProfileBook.Services
{
    public static class LocalService
    {
        // local user path
        private static string user_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "user_data.dat");
        // local user settings path
        private static string settings_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "settings.config");

        /// <summary>
        /// Default settings
        /// </summary>
        /// <returns>Settings</returns>
        public static Models.Settings GetDefaultSettings()
        {
            var settings = new Models.Settings {
                Sort = SortType.Date,
                Theme = Theme.Light,
                Language = Language.English,
                UserId = App.CurrentUser.Id
            };
            return settings;
        }

        #region save

        /// <summary>
        /// save the local user's data to the local storage
        /// </summary>
        /// <param name="user">User Model</param>
        public static void SaveUser(User user)
        {
            delete_user();
            File.WriteAllText(user_path,
                $"{user.Id}|" +
                $"{user.Login}|" +
                $"{user.Password}");
            App.CurrentUser = user;
        }

        /// <summary>
        /// save the user's settings data to the local storage
        /// </summary>
        /// <param name="settings">Settings Model</param>
        public static void SaveSettings(Models.Settings settings)
        {
            delete_settings();
            File.WriteAllText(settings_path,
                    $"{settings.Id}|" +
                    $"{settings.Sort}|" +
                    $"{settings.Theme}|" +
                    $"{settings.Language}|" +
                    $"{settings.UserId}");
            App.CurrentSettings = settings;
        }
        #endregion

        #region read

        /// <summary>
        /// read the local user's data from the local storage
        /// </summary>
        /// <returns>User Model</returns>
        public static User ReadUser()
        {
            if (!File.Exists(user_path)) {
                return null;
            }

            //parse data from the file
            string user_data = File.ReadAllText(user_path);
            string[] data = user_data.Split('|');

            try {
                var user = new User {
                    Id = Int32.Parse(data[0]),
                    Login = data[1],
                    Password = data[2],
                };
                return user;
            }
            catch (IndexOutOfRangeException) {
                return null;
            }
        }

        /// <summary>
        /// read the local user's settings data from the local storage
        /// </summary>
        /// <returns>Settings Model</returns>
        public static Models.Settings ReadSettings()
        {
            if (!File.Exists(settings_path)) {
                return null;
            }

            //parse data from the file
            string settings_data = File.ReadAllText(settings_path);
            string[] data = settings_data.Split('|');

            try {
                var settings = new Models.Settings {
                    Id = Int32.Parse(data[0]),
                    Sort = (SortType)Enum.Parse(typeof(SortType), data[1]),
                    Theme = (Theme)Enum.Parse(typeof(Theme), data[2]),
                    Language = (Enums.Language)Enum.Parse(typeof(Enums.Language), data[3]),
                    UserId = Int32.Parse(data[4])
                };
                return settings;
            }
            catch (IndexOutOfRangeException) {
                return null;
            }
        }

        #endregion

        #region delete

        /// <summary>
        /// delete the user's data from the local storage
        /// </summary>
        public static void Delete()
        {
            delete_user();
            delete_settings();
        }

        private static void delete_user()
        {
            if (File.Exists(user_path)) {
                File.Delete(user_path);
            }
            App.CurrentUser = null;
        }
        private static void delete_settings()
        {
            if (File.Exists(settings_path)) {
                File.Delete(settings_path);
            }
            App.CurrentSettings = null;
        }
        #endregion
    }
}
