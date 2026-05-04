using StarterApp.Database.Models;

namespace StarterApp.Services;

/// <summary>

/// Defines operations related to rental management within the application.

/// This service abstracts rental logic from ViewModels.

/// </summary>

public interface IRentalService

{

    /// <summary>

    /// Creates a new rental request for a given item.

    /// </summary>

    /// <param name="itemId">The ID of the item being rented.</param>

    /// <param name="borrowerId">The ID of the user requesting the rental.</param>

    /// <param name="startDate">The start date of the rental period.</param>

    /// <param name="endDate">The end date of the rental period.</param>

    /// <param name="dailyRate">The daily cost of the rental.</param>

    Task RequestRentalAsync(int itemId, string borrowerId, DateTime startDate, DateTime endDate, decimal dailyRate);

}
 