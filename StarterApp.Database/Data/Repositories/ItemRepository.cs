using Microsoft.EntityFrameworkCore;
using StarterApp.Database.Models;
namespace StarterApp.Database.Data.Repositories;
/// <summary>
/// Implements data access operations for Item entities using Entity Framework Core.
/// Provides CRUD functionality as part of the Repository pattern.
/// </summary>
public class ItemRepository : IItemRepository
{
   private readonly AppDbContext _context;
   public ItemRepository(AppDbContext context)
   {
       _context = context;
   }
   /// <summary>
   /// Retrieves all items from the database.
   /// </summary>
   public async Task<List<Item>> GetAllAsync()
   {
       return await _context.Items.ToListAsync();
   }
   /// <summary>
   /// Retrieves an item by its ID.
   /// Returns null if not found.
   /// </summary>
   public async Task<Item?> GetByIdAsync(int id)
   {
       return await _context.Items.FindAsync(id);
   }
   /// <summary>
   /// Adds a new item to the database.
   /// </summary>
   public async Task AddAsync(Item item)
   {
       _context.Items.Add(item);
       await _context.SaveChangesAsync();
   }
   /// <summary>
   /// Updates an existing item in the database.
   /// </summary>
   public async Task UpdateAsync(Item item)
   {
       _context.Items.Update(item);
       await _context.SaveChangesAsync();
   }
   /// <summary>
   /// Deletes an item from the database by ID.
   /// Does nothing if the item does not exist.
   /// </summary>
   public async Task DeleteAsync(int id)
   {
       var item = await _context.Items.FindAsync(id);
       if (item == null)
           return;
       _context.Items.Remove(item);
       await _context.SaveChangesAsync();
   }
}