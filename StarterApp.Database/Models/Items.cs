namespace StarterApp.Database.Models;

public class Item
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal DailyRate { get; set; }

    public string Category { get; set; } = string.Empty;

    public string OwnerId { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}