using Prism.Services;
using Prism.Services.Dialogs;
using ProfileBook.Dialogs;
using ProfileBook.Enums;
using ProfileBook.Services.Repository;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProfileBook.Properties;

namespace ProfileBook.Services.Main
{
    public class MainService : IMainService
    {
        private readonly IRepository<Models.Profile> profileRepository;
        private readonly IRepository<Models.Settings> settingsRepository;
        private readonly IPageDialogService pageDialogService;
        private readonly IDialogService dialogService;

        // get order by sort type for sql command
        private string GetOrderBySortType(SortType type)
        {
            switch (type) {
                case SortType.Name:
                    return "Name";
                case SortType.NickName:
                    return "NickName";
                case SortType.Date:
                default:
                    return "CreationDate DESC";
            }
        }

        public async Task<Models.Settings> GetSettings()
        {
            // get settings from database
            string sqlCommand = $"SELECT * FROM Settings WHERE UserId='{App.CurrentUser.Id}'";
            Models.Settings result = await settingsRepository.FindWithCommand(sqlCommand);

            if (result != null) {
                return result;
            }

            // if result == null -> get the default settings
            result = LocalService.GetDefaultSettings();
            await settingsRepository.Add(result);

            return result;
        }


        public async Task<IEnumerable<Models.Profile>> GetProfiles(int user_id)
        {
            if (App.CurrentSettings == null) {
                App.CurrentSettings = await GetSettings();
            }

            string sqlCommand = $"SELECT * FROM Profiles WHERE UserId='{App.CurrentUser.Id}' ORDER BY {GetOrderBySortType(App.CurrentSettings.Sort)}";
            return await profileRepository.GetAllWithCommand(sqlCommand);
        }

        public async Task<bool> RemoveProfile(Models.Profile profile)
        {
            if (await pageDialogService.DisplayAlertAsync(AppResources.MainDelete, AppResources.MainDeleteConfirm, AppResources.Accept, AppResources.Denie)) {

                //remove the local copy of the image
                if (File.Exists(profile.Image))
                    File.Delete(profile.Image);

                //remove from database
                await profileRepository.Remove(profile);
                return true;
            }
            return false;
        }

        public async Task ShowImage(string image_path)
        {
            await dialogService.ShowDialogAsync(nameof(ShowImageDialog), new DialogParameters {
                {"Image", ImageSource.FromFile(image_path) }
            });
        }

        public MainService(IRepository<Models.Profile> profileRepository, IRepository<Models.Settings> settingsRepository, IPageDialogService pageDialogService, IDialogService dialogService)
        {
            this.profileRepository = profileRepository;
            this.settingsRepository = settingsRepository;
            this.pageDialogService = pageDialogService;
            this.dialogService = dialogService;
        }
    }
}
