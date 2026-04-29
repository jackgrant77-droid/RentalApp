namespace StarterApp.Services;
public class LocationService : ILocationService
{
   public async Task<Location?> GetCurrentLocationAsync()
   {
       try
       {
           var request = new GeolocationRequest(
               GeolocationAccuracy.Medium,
               TimeSpan.FromSeconds(10));
           var location = await Geolocation.Default.GetLocationAsync(request);
           if (location is not null)
           {
               return location;
           }
       }
       catch
       {
           // Emulator GPS may fail, so use fallback below
       }
       return new Location(55.9533, -3.1883);
   }
}