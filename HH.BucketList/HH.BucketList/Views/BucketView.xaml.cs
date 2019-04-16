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
	public partial class BucketView : ContentPage
	{
        private BucketsInMemoryService bucketService;
        private AppSettingsInMemoryService settingsService;
        private BucketL currentBucketL;

		public BucketView (BucketL bucketL)
		{
			InitializeComponent ();

            settingsService = new AppSettingsInMemoryService();
            bucketService = new BucketsInMemoryService();

            if (bucketL == null)
            {
                currentBucketL = new BucketL();
                Title = "New Bucket List";
            }
            else
            {
                currentBucketL = bucketL;
                Title = currentBucketL.Title;
            }
		}

        protected override void OnAppearing()
        {
            LoadBucketState();
            base.OnAppearing();
        }

        private void LoadBucketState()
        {
            txtTitle.Text = currentBucketL.Title;
            txtDescription.Text = currentBucketL.Description;
            swIsFavorite.IsToggled = currentBucketL.IsFavorite;
            lblPercentComplete.Text = currentBucketL.PercentCompleted.ToString("P0");
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            SaveBucketState();
            busyIndicator.IsVisible = true;
            await bucketService.SaveBucketList(currentBucketL);
            busyIndicator.IsVisible = false;
            await DisplayAlert("Saved", $"Your bucket list {currentBucketL.Title} has been saved", "Ok");
        }

        private void SaveBucketState()
        {
            currentBucketL.Title = txtTitle.Text;
            currentBucketL.Description = txtDescription.Text;
            currentBucketL.IsFavorite = swIsFavorite.IsToggled;
        }
    }
}