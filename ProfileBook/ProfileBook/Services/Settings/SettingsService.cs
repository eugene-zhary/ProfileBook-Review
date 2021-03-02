using ProfileBook.Services.Repository;
using System.Threading.Tasks;

namespace ProfileBook.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private IRepository<Models.Settings> repository;

        public SettingsService(IRepository<Models.Settings> repository)
        {
            this.repository = repository;
        }

        public async Task UpdateSettings(Models.Settings settings)
        {
            await repository.AddOrUpdata(settings);
            LocalService.SaveSettings(settings);
        }
    }
}
