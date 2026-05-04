using StarterApp.Database.Models;
namespace StarterApp.Database.Data.Repositories;
/// <summary>
/// Defines data access operations for Rental entities.
/// Part of the Repository pattern to abstract database interactions.
/// </summary>
public interface IRentalRepository
{
   /// <summary>
   /// Retrieves all rentals from the database.
   /// </summary>
   Task<List<Rental>> GetAllAsync();
   /// <summary>
   /// Retrieves a specific rental by its ID.
   /// Returns null if the rental is not found.
   /// </summary>
   Task<Rental?> GetByIdAsync(int id);
   /// <summary>
   /// Adds a new rental record to the database.
   /// </summary>
   Task AddAsync(Rental rental);
   /// <summary>
   /// Updates an existing rental in the database.
   /// </summary>
   Task UpdateAsync(Rental rental);
}
