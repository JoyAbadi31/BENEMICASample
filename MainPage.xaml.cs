using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media; 

namespace BENEMICASample
{
    public partial class MainPage : ContentPage
    {
        public string[] MenuOptions { get; }

        public MainPage()
        {
            InitializeComponent();

            // Set default greeting
            GreetingLabel.Text = $"Hello, {Environment.UserName}!";

            // Dropdown menu options
            MenuOptions = new[] { "Home", "Profile", "Settings" };
            BindingContext = this;

            // Handle Dropdown selection change
            DropdownMenu.SelectedIndexChanged += (sender, e) =>
            {
                if (DropdownMenu.SelectedIndex != -1)
                {
                    SelectedOptionLabel.Text = $"Selected Option: {DropdownMenu.SelectedItem}";
                }
            };

            // Handle DatePicker selection change
            DatePickerControl.DateSelected += (sender, e) =>
            {
                SelectedDateLabel.Text = $"Selected Date: {e.NewDate.ToShortDateString()}";
            };
        }

        // Handle Upload Image button click
        private async void OnUploadImageButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select an image"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    SelectedImage.Source = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to upload image: {ex.Message}", "OK");
            }
        }

        // Handle Submit button click
        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string email = EmailEntry.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Error", "Name and email are required.", "OK");
                return;
            }

            await DisplayAlert("Form Submitted", $"Thank you, {name}! Your form has been submitted.", "OK");

            // Clear form
            NameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            SelectedImage.Source = null;
        }

        // Handle Open Maps button click
        private async void OnOpenMapsButtonClicked(object sender, EventArgs e)
        {
            // Navigate to Maps.xaml
            await Navigation.PushAsync(new MapsPage());
        }
    }
}
