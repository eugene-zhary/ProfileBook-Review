using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Profile
{
    public interface IProfileManager
    {
        /// <summary>
        /// Save the profile to the database
        /// </summary>
        /// <param name="profile">Profile Model</param>
        /// <returns>if added successfuly returns true otherwise returns false</returns>
        Task SaveProfile(Models.Profile profile);

        Task<IEnumerable<Models.Profile>> GetProfiles(int user_id);

        Task RemoveProfile(Models.Profile profile);
    }
}
