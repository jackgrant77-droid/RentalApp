using StarterApp.Database.Models;

namespace StarterApp.Services;

public interface IRentalService
{
    Task RequestRentalAsync(int itemId, string borrowerId, DateTime startDate, DateTime endDate, decimal dailyRate);
}