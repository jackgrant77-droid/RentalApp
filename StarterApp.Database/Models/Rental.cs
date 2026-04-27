namespace StarterApp.Database.Models;


public class Rental
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public Item? Item { get; set; }

    public string BorrowerId { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TotalPrice { get; set; }

    public RentalStatus Status { get; set; } = RentalStatus.Requested;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ApprovedAt { get; set; }

    public DateTime? ReturnedAt { get; set; }
}