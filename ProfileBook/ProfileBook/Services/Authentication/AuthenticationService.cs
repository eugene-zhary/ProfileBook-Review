using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IRepository<User> repository;
        private IPageDialogService pageDialogService;

        public AuthenticationService(IRepository<User> repository, IPageDialogService pageDialogService)
        {
            this.repository = repository;
            this.pageDialogService = pageDialogService;
        }

        public async Task<bool> SignIn(string login, string password)
        {
            User user = await repository.FindWithCommand($"SELECT * FROM Users WHERE Login='{login}' AND Password='{password}'");

            if (user != null) {
                LocalService.SaveUser(user);
                return true;
            }

            await pageDialogService.DisplayAlertAsync("SignIn", "Invalid login or password!", "OK");
            return false;
        }
    }
}
