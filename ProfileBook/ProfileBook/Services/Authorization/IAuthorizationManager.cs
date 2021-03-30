using System.Threading.Tasks;

namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationManager
    {
        /// <summary>
        /// authorizate new user
        /// </summary>
        /// <param name="login">sign up login</param>
        /// <param name="password">sign up password</param>
        /// <param name="confirmPassword">sign up confirm password</param>
        /// <returns>returns true if successfully authorizate new user, otherwise returns false</returns>
        Task<bool> RegUser(string login, string password, string confirmPassword);

        /// <summary>
        /// user's authenticate
        /// </summary>
        /// <param name="login">sing in login</param>
        /// <param name="password">sing in password</param>
        /// <returns>returns true if user authenticate successfully, otherwise returns false</returns>
        Task<bool> SignIn(string login, string password);
    }
}
