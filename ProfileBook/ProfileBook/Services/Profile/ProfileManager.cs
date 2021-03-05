using ProfileBook.Services.Repository;
using System.Threading.Tasks;
using ProfileBook.Services.Settings;
using System.Collections.Generic;
using System.IO;

namespace ProfileBook.Services.Profile
{
    public class ProfileManager : IProfileManager
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IRepository<Models.Profile> _repository;

        public ProfileManager(IRepository<Models.Profile> repository, ISettingsManager settingsManager)
        {
            this._settingsManager = settingsManager;
            this._repository = repository;
        }

        public async Task SaveProfile(Models.Profile profile)
        {
            _settingsManager.IsListUpdated = true;
            await _repository.AddOrUpdata(profile);
        }

        public async Task<IEnumerable<Models.Profile>> GetProfiles(int user_id)
        {
            string sqlCommand = $"SELECT * FROM Profiles WHERE UserId='{user_id}' ORDER BY {_settingsManager.SortType}";
            return await _repository.GetAllWithCommand(sqlCommand);
        }

        public async Task RemoveProfile(Models.Profile profile)
        {
            if (File.Exists(profile.Image))
                File.Delete(profile.Image);

            await _repository.Remove(profile);
        }
    }
}
