using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Settings
{
    public interface ISettingsService
    {
        /// <summary>
        /// update settings in the database
        /// </summary>
        /// <param name="settings">Settings model</param>
        /// <returns>Task</returns>
        Task UpdateSettings(Models.Settings settings);
    }
}
