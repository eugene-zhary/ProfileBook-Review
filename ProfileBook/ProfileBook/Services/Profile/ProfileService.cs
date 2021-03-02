using Prism.Services;
using Prism.Services.Dialogs;
using ProfileBook.Dialogs;
using ProfileBook.Services.Repository;
using ProfileBook.Validators;
using System;
using System.Threading.Tasks;
using ProfileBook.Properties;
using ProfileBook.Localization;

namespace ProfileBook.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Models.Profile> repository;
        private readonly IPageDialogService pageDialogService;
        private readonly IDialogService dialogService;
        private readonly LocalizedResources resources;

        public ProfileService(IRepository<Models.Profile> repository, IPageDialogService pageDialogService, IDialogService dialogService)
        {
            resources = new LocalizedResources(typeof(AppResources), App.CurrentSettings.Language);

            this.repository = repository;
            this.pageDialogService = pageDialogService;
            this.dialogService = dialogService;
        }

        public async Task<string> GetImagePath(Models.Profile profile)
        {
            IDialogResult result = await dialogService.ShowDialogAsync(nameof(PickImageDialog));
            string img_path = result.Parameters.GetValue<string>("ImagePath");

            if (img_path != null) {
                profile.Image = img_path;
                await repository.Update(profile);

                return img_path;
            }

            return null;
        }

        public async Task<bool> SaveProfile(Models.Profile profile)
        {
            string hints = ValidationHints.GetProfileHints(profile, resources);

            if (!hints.Equals(String.Empty)) {
                await pageDialogService.DisplayAlertAsync(resources["AddEditAlertTitle"], hints, resources["Confirm"]);
                return false;
            }

            await repository.AddOrUpdata(profile);
            return true;
        }
    }
}
