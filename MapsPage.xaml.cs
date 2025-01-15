using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace BENEMICASample
{
    public partial class MapsPage : ContentPage
    {
        public MapsPage()
        {
            InitializeComponent();
            LoadCurrentLocationAsync();
        }

        private async void LoadCurrentLocationAsync()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Best,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                if (location != null)
                {
                    var userPosition = new Location(location.Latitude, location.Longitude);
                    Map.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Location(userPosition.Latitude, userPosition.Longitude),
                        Distance.FromMeters(1000))); 

                    var pin = new Pin
                    {
                        Label = "Your Location",
                        Address = "You are here",
                        Type = PinType.Place,
                        Location = userPosition
                    };
                    Map.Pins.Add(pin);
                }
                else
                {
                    await DisplayAlert("Error", "Unable to fetch location. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}