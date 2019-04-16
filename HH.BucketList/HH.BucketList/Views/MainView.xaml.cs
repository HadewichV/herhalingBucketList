using HH.BucketList.Domain.Models;
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
	public partial class MainView : ContentPage
	{
        BucketsInMemoryService bucketListService;
        AppSettingsInMemoryService settingsService;

		public MainView ()
		{
			InitializeComponent ();
            settingsService = new AppSettingsInMemoryService();
            bucketListService = new BucketsInMemoryService();
		}

        protected async override void OnAppearing()
        {
            await RefreshBucketLists();            
            base.OnAppearing();
        }

        private async Task RefreshBucketLists()
        {
            busyIndicator.IsVisible = true;

            lvBucketLists.ItemsSource = null;
            //get settings, because we need current user Id 
            var settings = await settingsService.GetSettings();
            //get all bucket lists for this user 
            var buckets = await bucketListService.GetBucketListsForUser(settings.CurrentUserId); 
            //bind IEnumerable<Bucket> to the ListView's ItemSource 
            lvBucketLists.ItemsSource = buckets;

            busyIndicator.IsVisible = false;
        }

        private async void btnAddBucketList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BucketView(null));
        }

        private async void btnSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsView());
        }

        private async void LvBucketLists_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //get the item on which we received a tap
            var bucketL = e.Item as BucketL;
            
            if (bucketL != null)
            {
                await DisplayAlert("Tap!", $"Congratulations!\nYou tapped {bucketL.Title}", "Uh, ok..");
                await Navigation.PushAsync(new BucketView(bucketL));
            }
        }

        private async void MnuBucketEdit_Clicked(object sender, EventArgs e)
        {
            var selectedBucketL = ((MenuItem)sender).CommandParameter as BucketL;
            await DisplayAlert("Edit", $"Editing { selectedBucketL.Title}", "OK");

            await Navigation.PushAsync(new BucketView(selectedBucketL));
        }

        private async void MnuBucketDelete_Clicked(object sender, EventArgs e)
        {
            var selectedBucketL = ((MenuItem)sender).CommandParameter as BucketL;
            await bucketListService.DeleteBucketList(selectedBucketL.Id);
            await RefreshBucketLists();
        }
    }
}