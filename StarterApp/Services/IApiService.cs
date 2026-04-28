using StarterApp.Database.Models;

namespace StarterApp.Services;

public interface IApiService
{
    Task<List<Item>> GetItemsAsync();

    Task<List<Rental>> GetOutgoingRentalsAsync();

    Task<Item> CreateItemAsync(Item item);

    Task RequestRentalAsync(int itemId, DateTime startDate, DateTime endDate);
}