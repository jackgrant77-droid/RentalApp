using StarterApp.Database.Models;
namespace StarterApp.Services;
/// <summary>
/// Defines authentication-related operations such as login, registration,
/// logout, role checking, and password management.
/// </summary>
public interface IAuthenticationService
{
   /// <summary>
   /// Event triggered when the authentication state changes (login or logout).
   /// </summary>
   event EventHandler<bool>? AuthenticationStateChanged;
   /// <summary>
   /// Indicates whether a user is currently authenticated.
   /// </summary>
   bool IsAuthenticated { get; }
   /// <summary>
   /// Gets the currently authenticated user, or null if not logged in.
   /// </summary>
   User? CurrentUser { get; }
   /// <summary>
   /// Gets the list of roles assigned to the current user.
   /// </summary>
   List<string> CurrentUserRoles { get; }
   /// <summary>
   /// Attempts to log in a user using their email and password.
   /// </summary>
   Task<AuthenticationResult> LoginAsync(string email, string password);
   /// <summary>
   /// Registers a new user with the provided details.
   /// </summary>
   Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password);
   /// <summary>
   /// Logs out the current user and clears authentication state.
   /// </summary>
   Task LogoutAsync();
   /// <summary>
   /// Checks if the current user has a specific role.
   /// </summary>
   bool HasRole(string roleName);
   /// <summary>
   /// Checks if the current user has at least one of the specified roles.
   /// </summary>
   bool HasAnyRole(params string[] roleNames);
   /// <summary>
   /// Checks if the current user has all of the specified roles.
   /// </summary>
   bool HasAllRoles(params string[] roleNames);
   /// <summary>
   /// Changes the current user's password after verifying the existing password.
   /// </summary>
   Task<bool> ChangePasswordAsync(string currentPassword, string newPassword);
}