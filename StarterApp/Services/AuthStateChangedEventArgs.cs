using StarterApp.Database.Models;
namespace StarterApp.Services;
/// <summary>
/// Provides data for authentication state change events.
/// Used to notify subscribers when a user logs in or logs out.
/// </summary>
public class AuthStateChangedEventArgs : EventArgs
{
   /// <summary>
   /// Indicates whether the user is currently authenticated.
   /// </summary>
   public bool IsAuthenticated { get; set; }
   /// <summary>
   /// Stores the authenticated user (null if logged out).
   /// </summary>
   public User? User { get; set; }
   /// <summary>
   /// Stores the roles associated with the authenticated user.
   /// </summary>
   public List<string> Roles { get; set; } = new();
}