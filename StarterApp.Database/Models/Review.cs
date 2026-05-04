namespace StarterApp.Database.Models;

/// <summary>

/// Represents a review submitted by a user after completing a rental.

/// Contains rating and feedback information.

/// </summary>

public class Review

{

    /// <summary>

    /// Unique identifier for the review.

    /// </summary>

    public int Id { get; set; }

    /// <summary>

    /// ID of the rental associated with this review.

    /// </summary>

    public int RentalId { get; set; }

    /// <summary>

    /// ID of the item being reviewed.

    /// </summary>

    public int ItemId { get; set; }

    /// <summary>

    /// ID of the user who submitted the review.

    /// </summary>

    public int ReviewerId { get; set; }

    /// <summary>

    /// Name of the reviewer.

    /// </summary>

    public string ReviewerName { get; set; } = string.Empty;

    /// <summary>

    /// Rating given to the item (e.g., 1–5).

    /// </summary>

    public int Rating { get; set; }

    /// <summary>

    /// Written feedback provided by the reviewer.

    /// </summary>

    public string Comment { get; set; } = string.Empty;

    /// <summary>

    /// Timestamp when the review was created.

    /// </summary>

    public DateTime CreatedAt { get; set; }

}
 