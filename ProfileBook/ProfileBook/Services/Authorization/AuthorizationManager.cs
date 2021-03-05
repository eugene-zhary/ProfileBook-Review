using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProfileBook.Services.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private IRepository<User> _repository;

        public AuthorizationManager(IRepository<User> repository)
        {
            this._repository = repository;
        }

        public async Task<bool> SignIn(string login, string password)
        {
            User user = await _repository.FindWithCommand($"SELECT * FROM Users WHERE Login='{login}' AND Password='{password}'");

            if (user != null) {
                Preferences.Set("UserId", user.Id);
                return true;
            }

            return false;
        }

        public async Task<bool> RegUser(string login, string password, string confirmPassword)
        {
            // check for the unique login
            User user = await _repository.FindWithCommand($"SELECT * FROM Users WHERE Login='{login}'");
            if (user != null) {
                return false;
            }
            
            //add the new user
            var new_user = new User() {
                Login = login,
                Password = password
            };

            await _repository.Add(new_user);
            return true;
        }
    }
}
