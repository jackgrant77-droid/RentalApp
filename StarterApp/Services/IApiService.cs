using StarterApp.Database.Models;

namespace StarterApp.Services;

public interface IApiService
{
    Task<List<Item>> GetItemsAsync();

    Task<List<Item>> GetNearbyItemsAsync(double latitude, double longitude, double radiusKm);

    Task<List<Rental>> GetOutgoingRentalsAsync();

    Task<Item> CreateItemAsync(Item item);

    Task RequestRentalAsync(int itemId, DateTime startDate, DateTime endDate);

    Task UpdateRentalStatusAsync(int rentalId, string status);
    Task SubmitReviewAsync(int rentalId, int rating, string comment);
    Task<List<Review>> GetItemReviewsAsync(int itemId);
    
}

