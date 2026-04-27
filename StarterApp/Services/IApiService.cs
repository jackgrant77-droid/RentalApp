using StarterApp.Database.Models;

namespace StarterApp.Services;

public interface IApiService
{
    Task<List<Item>> GetItemsAsync();

    Task<Item> CreateItemAsync(Item item);

    Task RequestRentalAsync(int itemId, DateTime startDate, DateTime endDate);
}