using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ProfileBook.Properties;
using ProfileBook.Localization;

namespace ProfileBook.Validators
{
    public static class ValidationHints
    {
        /// <summary>
        /// uses for sign up validation
        /// </summary>
        /// <param name="login">sign up login</param>
        /// <param name="password">sign up password</param>
        /// <param name="confirmPassword">sign up confirm password</param>
        /// <returns>hints for sign up</returns>
        public static string GetSignUpHints(string login, string password, string confirmPassword)
        {
            string alert = String.Empty;

            // Login: Minimum 4 chars, maximum 16 chars. Can not starts with number
            if (!Regex.IsMatch(login, @"^[a-zA-Z][a-zA-Z0-9]{3,16}$")) {
                alert +=  "Login:\n * Minimum 4 chars, maximum 16 chars.\n * Can not starts with number\n";
            }

            // Passsword: Minimum 8 chars, maximum 16 chars. At least 1 uppercase letter, 1 lowercase letter and 1 number
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$")) {
                alert += "Passsword:\n * Minimum 8 chars, maximum 16 chars.\n * At least 1 uppercase letter, 1 lowercase letter and 1 number\n";
            }

            // Confirm password: Equals password
            if (!confirmPassword.Equals(password)) {
                alert += "Confirm password:\n * Must match the password\n";
            }

            return alert;
        }

        /// <summary>
        /// uses for add, edit validation
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="resources">Current LocalizadResources</param>
        /// <returns>hits for add, update profile data</returns>
        public static string GetProfileHints(Profile profile, LocalizedResources resources)
        {
            string alert = String.Empty;

            if (profile.Name == null || profile.Name.Equals(String.Empty)) {
                alert += resources["AddEditValidateName"];
            }
            if (profile.NickName == null || profile.NickName.Equals(String.Empty)) {
                alert += resources["AddEditValidateNickName"];
            }

            return alert;
        }
    }
}
