using StarterApp.Database.Models;
namespace StarterApp.Database.Data.Repositories;
/// <summary>
/// Defines data access operations for Item entities.
/// Implements the Repository pattern to abstract database interactions.
/// </summary>
public interface IItemRepository
{
   /// <summary>
   /// Retrieves all items from the database.
   /// </summary>
   Task<List<Item>> GetAllAsync();
   /// <summary>
   /// Retrieves a specific item by its ID.
   /// Returns null if the item is not found.
   /// </summary>
   Task<Item?> GetByIdAsync(int id);
   /// <summary>
   /// Adds a new item to the database.
   /// </summary>
   Task AddAsync(Item item);
   /// <summary>
   /// Updates an existing item in the database.
   /// </summary>
   Task UpdateAsync(Item item);
   /// <summary>
   /// Deletes an item from the database using its ID.
   /// </summary>
   Task DeleteAsync(int id);
}