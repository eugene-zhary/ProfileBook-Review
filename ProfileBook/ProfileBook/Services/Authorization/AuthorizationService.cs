using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private IRepository<User> repository;
        private IPageDialogService pageDialogService;

        public AuthorizationService(IRepository<User> repository, IPageDialogService pageDialogService)
        {
            this.repository = repository;
            this.pageDialogService = pageDialogService;
        }

        public async Task<bool> RegUser(string login, string password, string confirmPassword)
        {
            // check for unique login
            User user = await repository.FindWithCommand($"SELECT * FROM Users WHERE Login='{login}'");
            if (user != null) {
                await this.pageDialogService.DisplayAlertAsync("SignUp", "Login already exists", "OK");
                return false;
            }

            // validate
            string hints = Validators.ValidationHints.GetSignUpHints(login, password, confirmPassword);
            if (hints.Length > 0) {
                await this.pageDialogService.DisplayAlertAsync("SignUp", hints, "OK");
                return false;
            }
           
            //add new user
            var new_user = new User() {
                Login = login,
                Password = password
            };
            await repository.Add(new_user);
            return true;
        }
    }
}
