using StarterApp.Database.Models;

namespace StarterApp.Database.Data.Repositories;

public interface IRentalRepository
{
    Task<List<Rental>> GetAllAsync();

    Task<Rental?> GetByIdAsync(int id);

    Task AddAsync(Rental rental);

    Task UpdateAsync(Rental rental);
}
