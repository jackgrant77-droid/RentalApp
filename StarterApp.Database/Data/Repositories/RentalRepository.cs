using Microsoft.EntityFrameworkCore;
using StarterApp.Database.Models;
namespace StarterApp.Database.Data.Repositories;
/// <summary>
/// Implements data access operations for Rental entities using Entity Framework Core.
/// Handles retrieval and persistence of rental data, including related item information.
/// </summary>
public class RentalRepository : IRentalRepository
{
   private readonly AppDbContext _context;
   public RentalRepository(AppDbContext context)
   {
       _context = context;
   }
   /// <summary>
   /// Retrieves all rentals from the database, including associated item details.
   /// </summary>
   public async Task<List<Rental>> GetAllAsync()
   {
       return await _context.Rentals
           .Include(r => r.Item) // Load related item data
           .ToListAsync();
   }
   /// <summary>
   /// Retrieves a specific rental by its ID, including associated item details.
   /// Returns null if not found.
   /// </summary>
   public async Task<Rental?> GetByIdAsync(int id)
   {
       return await _context.Rentals
           .Include(r => r.Item) // Load related item data
           .FirstOrDefaultAsync(r => r.Id == id);
   }
   /// <summary>
   /// Adds a new rental record to the database.
   /// </summary>
   public async Task AddAsync(Rental rental)
   {
       _context.Rentals.Add(rental);
       await _context.SaveChangesAsync();
   }
   /// <summary>
   /// Updates an existing rental record in the database.
   /// </summary>
   public async Task UpdateAsync(Rental rental)
   {
       _context.Rentals.Update(rental);
       await _context.SaveChangesAsync();
   }
}