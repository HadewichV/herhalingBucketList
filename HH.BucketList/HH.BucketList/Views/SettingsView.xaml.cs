using HH.BucketList.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HH.BucketList.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsView : ContentPage
	{
        AppSettingsInMemoryService settingsService;
        UsersInMemoryService usersService;

		public SettingsView ()
		{
			InitializeComponent ();
            settingsService = new AppSettingsInMemoryService();
            usersService = new UsersInMemoryService();
		}

        protected async override void OnAppearing()
        {
            //get settings and initialize controls
            var settings = await settingsService.GetSettings();
            swEnableListSharing.On = settings.EnableListSharing;
            swEnableNotifications.On = settings.EnableNotifications;

            //get current Uer and initialize controls
            var currentUser = await usersService.GetUserById(settings.CurrentUserId);
            txtUserName.Text = currentUser.UserName;
            txtEmail.Text = currentUser.Email;

            base.OnAppearing();
        }

        private async void btnSaveSettings_Clicked(object sender, EventArgs e)
        {
            //save app settings 
            var currentSettings = await settingsService.GetSettings();
            currentSettings.EnableListSharing = swEnableListSharing.On;
            currentSettings.EnableNotifications = swEnableNotifications.On;
            await settingsService.SaveSettings(currentSettings);

            //save user info settings
            var user = await usersService.GetUserById(currentSettings.CurrentUserId);
            user.UserName = txtUserName.Text.Trim();
            user.Email = txtEmail.Text.Trim(); 
            await usersService.SaveUser(user); 

            //close settings page 
            await Navigation.PopAsync();
        }
    }
}