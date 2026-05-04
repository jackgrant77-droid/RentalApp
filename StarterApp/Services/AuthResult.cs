using StarterApp.Database.Models;
namespace StarterApp.Services;
/// <summary>
/// Represents the result of an authentication operation.
/// Stores whether the operation succeeded, any error message,
/// the authenticated user, and the user's assigned roles.
/// </summary>
public class AuthResult
{
   /// <summary>
   /// Indicates whether the authentication operation was successful.
   /// </summary>
   public bool IsSuccess { get; set; }
   /// <summary>
   /// Stores an error message when authentication fails.
   /// </summary>
   public string ErrorMessage { get; set; } = string.Empty;
   /// <summary>
   /// Stores the authenticated user when authentication succeeds.
   /// </summary>
   public User? User { get; set; }
   /// <summary>
   /// Stores the roles assigned to the authenticated user.
   /// </summary>
   public List<string> Roles { get; set; } = new();
   /// <summary>
   /// Creates a successful authentication result.
   /// </summary>
   public static AuthResult Success(User user, List<string> roles)
   {
       return new AuthResult
       {
           IsSuccess = true,
           User = user,
           Roles = roles
       };
   }
   /// <summary>
   /// Creates a failed authentication result with an error message.
   /// </summary>
   public static AuthResult Failure(string errorMessage)
   {
       return new AuthResult
       {
           IsSuccess = false,
           ErrorMessage = errorMessage
       };
   }
}