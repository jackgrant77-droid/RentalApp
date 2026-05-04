namespace StarterApp.Database.Models;
/// <summary>
/// Represents an item that can be listed and rented in the application.
/// Contains details such as pricing, location, and ownership.
/// </summary>
public class Item
{
   /// <summary>
   /// Unique identifier for the item.
   /// </summary>
   public int Id { get; set; }
   /// <summary>
   /// Title of the item.
   /// </summary>
   public string Title { get; set; } = string.Empty;
   /// <summary>
   /// Description providing details about the item.
   /// </summary>
   public string Description { get; set; } = string.Empty;
   /// <summary>
   /// Daily rental price for the item.
   /// </summary>
   public decimal DailyRate { get; set; }
   /// <summary>
   /// Category the item belongs to (e.g., tools, electronics).
   /// </summary>
   public string Category { get; set; } = string.Empty;
   /// <summary>
   /// ID of the user who owns the item.
   /// </summary>
   public string OwnerId { get; set; } = string.Empty;
   /// <summary>
   /// Latitude coordinate of the item's location.
   /// </summary>
   public double Latitude { get; set; }
   /// <summary>
   /// Longitude coordinate of the item's location.
   /// </summary>
   public double Longitude { get; set; }
   /// <summary>
   /// Timestamp indicating when the item was created.
   /// Defaults to the current UTC time.
   /// </summary>
   public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}