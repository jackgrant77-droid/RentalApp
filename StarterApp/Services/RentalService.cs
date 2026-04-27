using StarterApp.Database.Data.Repositories;
using StarterApp.Database.Models;

namespace StarterApp.Services;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;

    public RentalService(IRentalRepository rentalRepository)
    {
        _rentalRepository = rentalRepository;
    }

    public async Task RequestRentalAsync(int itemId, string borrowerId, DateTime startDate, DateTime endDate, decimal dailyRate)
    {
        if (endDate < startDate)
        {
            throw new InvalidOperationException("End date cannot be before start date.");
        }

        var days = (endDate.Date - startDate.Date).Days + 1;

        var rental = new Rental
        {
            ItemId = itemId,
            BorrowerId = borrowerId,
            StartDate = startDate,
            EndDate = endDate,
            TotalPrice = dailyRate * days,
            Status = RentalStatus.Requested
        };

        await _rentalRepository.AddAsync(rental);
    }
}