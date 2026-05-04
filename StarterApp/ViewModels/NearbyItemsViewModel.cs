using System.Collections.ObjectModel;

using System.Windows.Input;

using StarterApp.Database.Models;

using StarterApp.Services;

namespace StarterApp.ViewModels;

/// <summary>

/// ViewModel responsible for finding and displaying items near the user's location.

/// Uses the LocationService to get GPS coordinates and ApiService to retrieve nearby items.

/// </summary>

public class NearbyItemsViewModel

{

    private readonly IApiService _apiService;

    private readonly ILocationService _locationService;

    /// <summary>

    /// Collection of nearby items displayed in the UI.

    /// ObservableCollection updates the UI automatically when items are added.

    /// </summary>

    public ObservableCollection<Item> NearbyItems { get; } = new();

    /// <summary>

    /// Search radius in kilometres.

    /// Default value is 5km.

    /// </summary>

    public double RadiusKm { get; set; } = 5;

    /// <summary>

    /// Command triggered when the user searches for nearby items.

    /// </summary>

    public ICommand FindNearbyCommand { get; }

    public NearbyItemsViewModel(IApiService apiService, ILocationService locationService)

    {

        _apiService = apiService;

        _locationService = locationService;

        // Bind the button command to the nearby search method

        FindNearbyCommand = new Command(async () => await FindNearbyAsync());

    }

    /// <summary>

    /// Gets the user's current location, sends it to the API,

    /// and loads nearby items into the collection.

    /// </summary>

    private async Task FindNearbyAsync()

    {

        try

        {

            // Clear previous results before running a new search

            NearbyItems.Clear();

            // Get current GPS location from LocationService

            var location = await _locationService.GetCurrentLocationAsync();

            if (location is null)

            {

                await Application.Current.MainPage.DisplayAlert(

                    "Location Error",

                    "Could not get your current location.",

                    "OK");

                return;

            }

            // Retrieve nearby items from API using coordinates and radius

            var items = await _apiService.GetNearbyItemsAsync(

                location.Latitude,

                location.Longitude,

                RadiusKm);

            // Populate UI collection

            foreach (var item in items)

            {

                NearbyItems.Add(item);

            }

        }

        catch (Exception ex)

        {

            // Display error if location or API request fails

            await Application.Current.MainPage.DisplayAlert(

                "Error",

                ex.Message,

                "OK");

        }

    }

}
 