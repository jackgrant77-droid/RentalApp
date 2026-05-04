namespace StarterApp.Services;
/// <summary>
/// Defines functionality for retrieving the device's current location.
/// This abstraction allows location access to be mocked during testing.
/// </summary>
public interface ILocationService
{
   /// <summary>
   /// Retrieves the current geographic location of the device.
   /// Returns null if the location cannot be determined.
   /// </summary>
   Task<Location?> GetCurrentLocationAsync();
}