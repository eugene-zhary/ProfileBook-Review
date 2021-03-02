using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Profile
{
    public interface IProfileService
    {
        /// <summary>
        /// Save the profile to the database
        /// </summary>
        /// <param name="profile">Profile Model</param>
        /// <returns>if added successfuly returns true otherwise returns false</returns>
        Task<bool> SaveProfile(Models.Profile profile);

        /// <summary>
        /// Returns picked image from gallery or camera
        /// </summary>
        /// <param name="profile">Profile Model</param>
        /// <returns>image path</returns>
        Task<string> GetImagePath(Models.Profile profile);
    }
}
