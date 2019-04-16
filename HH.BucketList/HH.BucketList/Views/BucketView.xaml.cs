using FluentValidation;
using HH.BucketList.Domain.Models;
using HH.BucketList.Domain.Services;
using HH.BucketList.Domain.Validators;
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
        private IValidator bucketValidator;

		public BucketView (BucketL bucketL)
		{
			InitializeComponent ();

            settingsService = new AppSettingsInMemoryService();
            bucketService = new BucketsInMemoryService();
            bucketValidator = new BucketValidator();

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
            if (Validate(currentBucketL))
            {
                busyIndicator.IsVisible = true;
                await bucketService.SaveBucketList(currentBucketL);
                busyIndicator.IsVisible = false;
                await DisplayAlert("Saved", $"Your bucket list {currentBucketL.Title} has been saved", "Ok");
            }
        }

        private void SaveBucketState()
        {
            currentBucketL.Title = txtTitle.Text;
            currentBucketL.Description = txtDescription.Text;
            currentBucketL.IsFavorite = swIsFavorite.IsToggled;
        }

        private bool Validate(BucketL bucketL)
        {
            lblErrorTitle.IsVisible = false;
            lblErrorDescription.IsVisible = false;

            var validationResult = bucketValidator.Validate(bucketL);

            //loop through error to identify properties
            foreach(var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(bucketL.Title))
                {
                    lblErrorTitle.Text = error.ErrorMessage;
                    lblErrorTitle.IsVisible = true;
                }

                if (error.PropertyName == nameof(bucketL.Description))
                {
                    lblErrorDescription.Text = error.ErrorMessage;
                    lblErrorDescription.IsVisible = true;
                }
            }
            return validationResult.IsValid;

            //bool error = false;

            //if (string.IsNullOrWhiteSpace(bucketL.Title))
            //{
            //    error = true;
            //    lblErrorTitle.Text = "Title cannot be empty.";
            //    lblErrorTitle.IsVisible = true;
            //}

            //if (string.IsNullOrWhiteSpace(bucketL.Description))
            //{
            //    error = true;
            //    lblErrorDescription.Text = "Description cannot be empty.";
            //    lblErrorDescription.IsVisible = true;
            //}

            //if (!error)
            //{
            //    lblErrorTitle.Text = "";
            //    lblErrorTitle.IsVisible = false;
            //    lblErrorDescription.Text = "";
            //    lblErrorDescription.IsVisible = false;
            //}

            //return !error;
        }
    }
}