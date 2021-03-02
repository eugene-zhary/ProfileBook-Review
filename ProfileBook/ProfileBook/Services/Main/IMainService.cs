using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Main
{
    public interface IMainService
    {
        /// <summary>
        /// Returns all user's profiles
        /// </summary>
        /// <param name="user_id">user id</param>
        /// <returns>IEnumerable<Profile></returns>
        Task<IEnumerable<Models.Profile>> GetProfiles(int user_id);

        /// <summary>
        /// Remove profile from the database
        /// </summary>
        /// <param name="profile">Profile Model</param>
        /// <returns>Returns true if removed successfully otherwise retuns false</returns>
        Task<bool> RemoveProfile(Models.Profile profile);

        /// <summary>
        /// Show image modaly
        /// </summary>
        /// <param name="image_path">Image Path</param>
        Task ShowImage(string image_path);

        /// <summary>
        /// get user sattings or generate default
        /// </summary>
        /// <returns>Settings Model</returns>
        Task<Models.Settings> GetSettings();
    }
}
