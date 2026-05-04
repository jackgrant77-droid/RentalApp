using StarterApp.Database.Data.Repositories;

using StarterApp.Database.Models;

namespace StarterApp.Services;

/// <summary>

/// Handles rental-related business logic such as creating rental requests.

/// This service separates rental logic from ViewModels and controllers.

/// </summary>

public class RentalService : IRentalService

{

    private readonly IRentalRepository _rentalRepository;

    public RentalService(IRentalRepository rentalRepository)

    {

        _rentalRepository = rentalRepository;

    }

    /// <summary>

    /// Creates a new rental request and calculates the total rental price.

    /// </summary>

    /// <param name="itemId">The ID of the item being rented.</param>

    /// <param name="borrowerId">The ID of the user requesting the rental.</param>

    /// <param name="startDate">The start date of the rental period.</param>

    /// <param name="endDate">The end date of the rental period.</param>

    /// <param name="dailyRate">The daily cost of the rental.</param>

    public async Task RequestRentalAsync(int itemId, string borrowerId, DateTime startDate, DateTime endDate, decimal dailyRate)

    {

        // Validate that the rental dates are logical

        if (endDate < startDate)

        {

            throw new InvalidOperationException("End date cannot be before start date.");

        }

        // Calculate number of rental days (inclusive)

        var days = (endDate.Date - startDate.Date).Days + 1;

        // Create rental object with calculated total price

        var rental = new Rental

        {

            ItemId = itemId,

            BorrowerId = borrowerId,

            StartDate = startDate,

            EndDate = endDate,

            TotalPrice = dailyRate * days,

            Status = RentalStatus.Requested

        };

        // Save rental to the database via repository

        await _rentalRepository.AddAsync(rental);

    }

}
 