using System.Threading.Tasks;

namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        /// <summary>
        /// authorizate new user
        /// </summary>
        /// <param name="login">sign up login</param>
        /// <param name="password">sign up password</param>
        /// <param name="confirmPassword">sign up confirm password</param>
        /// <returns>returns true if successfully authorizate new user, otherwise returns false</returns>
        Task<bool> RegUser(string login, string password, string confirmPassword);
    }
}
