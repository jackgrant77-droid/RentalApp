namespace StarterApp.Services;

/// <summary>

/// Provides access to the device's current GPS location.

/// Includes a fallback location if GPS is unavailable (e.g., emulator).

/// </summary>

public class LocationService : ILocationService

{

    /// <summary>

    /// Attempts to retrieve the current device location.

    /// Returns a fallback location if the request fails.

    /// </summary>

    public async Task<Location?> GetCurrentLocationAsync()

    {

        try

        {

            // Configure location request with medium accuracy and timeout

            var request = new GeolocationRequest(

                GeolocationAccuracy.Medium,

                TimeSpan.FromSeconds(10));

            // Attempt to get current location from device

            var location = await Geolocation.Default.GetLocationAsync(request);

            if (location is not null)

            {

                return location;

            }

        }

        catch

        {

            // If location retrieval fails (e.g., emulator), fallback is used below

        }

        // Fallback location (Edinburgh) used when GPS is unavailable

        return new Location(55.9533, -3.1883);

    }

}
 