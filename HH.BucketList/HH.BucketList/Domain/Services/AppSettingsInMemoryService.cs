using HH.BucketList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HH.BucketList.Domain.Services
{
    public class AppSettingsInMemoryService
    {
        private static AppSettings currentSettings = new AppSettings
        {
            CurrentUserId = Guid.Empty, //refers to (first) local user 
            EnableListSharing = true,
            EnableNotifications = false
        };

        public async Task<AppSettings> GetSettings()
        {
            await Task.Delay(1000);
            return currentSettings;
        }

        public async Task SaveSettings(AppSettings settings)
        { 
            await Task.Delay(1000);
            currentSettings = settings;
        }
    }
}
