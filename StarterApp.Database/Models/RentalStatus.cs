namespace StarterApp.Database.Models;
/// <summary>
/// Represents the different states a rental can be in throughout its lifecycle.
/// </summary>
public enum RentalStatus
{
   /// <summary>
   /// Rental has been requested but not yet reviewed.
   /// </summary>
   Requested,
   /// <summary>
   /// Rental request has been approved by the owner.
   /// </summary>
   Approved,
   /// <summary>
   /// Rental request has been rejected.
   /// </summary>
   Rejected,
   /// <summary>
   /// Item is currently out for rent.
   /// </summary>
   OutForRent,
   /// <summary>
   /// Item has been returned by the borrower.
   /// </summary>
   Returned,
   /// <summary>
   /// Rental has been fully completed.
   /// </summary>
   Completed,
   /// <summary>
   /// Rental has exceeded the expected return date.
   /// </summary>
   Overdue
}