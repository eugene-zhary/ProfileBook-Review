using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Authentication
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// user's authenticate
        /// </summary>
        /// <param name="login">sing in login</param>
        /// <param name="password">sing in password</param>
        /// <returns>returns true if user authenticate successfully, otherwise returns false</returns>
        Task<bool> SignIn(string login, string password);
    }
}
