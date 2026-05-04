using StarterApp.Database.Models;
namespace StarterApp.Services;
/// <summary>
/// Defines methods for interacting with the external API,
/// including items, rentals, and reviews.
/// </summary>
public interface IApiService
{
   /// <summary>
   /// Retrieves all available items from the API.
   /// </summary>
   Task<List<Item>> GetItemsAsync();
   /// <summary>
   /// Retrieves items near a given location within a specified radius.
   /// </summary>
   Task<List<Item>> GetNearbyItemsAsync(double latitude, double longitude, double radiusKm);
   /// <summary>
   /// Retrieves rentals created by the current user.
   /// </summary>
   Task<List<Rental>> GetOutgoingRentalsAsync();
   /// <summary>
   /// Creates a new item listing in the system.
   /// </summary>
   Task<Item> CreateItemAsync(Item item);
   /// <summary>
   /// Sends a rental request for a specific item.
   /// </summary>
   Task RequestRentalAsync(int itemId, DateTime startDate, DateTime endDate);
   /// <summary>
   /// Updates the status of an existing rental.
   /// </summary>
   Task UpdateRentalStatusAsync(int rentalId, string status);
   /// <summary>
   /// Submits a review for a completed rental.
   /// </summary>
   Task SubmitReviewAsync(int rentalId, int rating, string comment);
   /// <summary>
   /// Retrieves all reviews for a specific item.
   /// </summary>
   Task<List<Review>> GetItemReviewsAsync(int itemId);
}

