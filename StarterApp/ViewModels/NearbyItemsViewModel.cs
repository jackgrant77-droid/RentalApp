using System.Collections.ObjectModel;
using System.Windows.Input;
using StarterApp.Database.Models;
using StarterApp.Services;

namespace StarterApp.ViewModels;

public class NearbyItemsViewModel
{
    private readonly IApiService _apiService;
    private readonly ILocationService _locationService;

    public ObservableCollection<Item> NearbyItems { get; } = new();

    public double RadiusKm { get; set; } = 5;

    public ICommand FindNearbyCommand { get; }

    public NearbyItemsViewModel(IApiService apiService, ILocationService locationService)
    {
        _apiService = apiService;
        _locationService = locationService;

        FindNearbyCommand = new Command(async () => await FindNearbyAsync());
    }

    private async Task FindNearbyAsync()
    {
        try
        {
            NearbyItems.Clear();

            var location = await _locationService.GetCurrentLocationAsync();

            if (location is null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Location Error",
                    "Could not get your current location.",
                    "OK");
                return;
            }

            var items = await _apiService.GetNearbyItemsAsync(
                location.Latitude,
                location.Longitude,
                RadiusKm);

            foreach (var item in items)
            {
                NearbyItems.Add(item);
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
    }
}