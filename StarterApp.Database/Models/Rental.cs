namespace StarterApp.Database.Models;

/// <summary>

/// Represents a rental transaction between a borrower and an item owner.

/// Includes rental dates, pricing, and status tracking.

/// </summary>

public class Rental

{

    /// <summary>

    /// Unique identifier for the rental.

    /// </summary>

    public int Id { get; set; }

    /// <summary>

    /// Foreign key linking to the rented item.

    /// </summary>

    public int ItemId { get; set; }

    /// <summary>

    /// Navigation property for the related item.

    /// </summary>

    public Item? Item { get; set; }

    /// <summary>

    /// ID of the user who is renting the item.

    /// </summary>

    public string BorrowerId { get; set; } = string.Empty;

    /// <summary>

    /// Start date of the rental period.

    /// </summary>

    public DateTime StartDate { get; set; }

    /// <summary>

    /// End date of the rental period.

    /// </summary>

    public DateTime EndDate { get; set; }

    /// <summary>

    /// Total cost of the rental calculated based on duration and daily rate.

    /// </summary>

    public decimal TotalPrice { get; set; }

    /// <summary>

    /// Current status of the rental (e.g., Requested, Approved, Returned, Completed).

    /// </summary>

    public RentalStatus Status { get; set; } = RentalStatus.Requested;

    /// <summary>

    /// Timestamp when the rental was requested.

    /// </summary>

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    /// <summary>

    /// Timestamp when the rental was approved (if applicable).

    /// </summary>

    public DateTime? ApprovedAt { get; set; }

    /// <summary>

    /// Timestamp when the item was returned (if applicable).

    /// </summary>

    public DateTime? ReturnedAt { get; set; }

}
 