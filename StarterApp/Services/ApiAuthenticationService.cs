using System.Text.Json;
using StarterApp.Database.Models;
using System.Net.Http.Json;
namespace StarterApp.Services;
/// <summary>
/// Service responsible for handling user authentication including login, logout,
/// and role-based access control using the external API.
/// </summary>
public class ApiAuthenticationService : IAuthenticationService
{
   // HttpClient configured with the base API URL
   private readonly HttpClient _httpClient = new()
   {
       BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
   };
   /// <summary>
   /// Event triggered when the authentication state changes (login/logout).
   /// </summary>
   public event EventHandler<bool>? AuthenticationStateChanged;
   /// <summary>
   /// Indicates whether a user is currently authenticated.
   /// </summary>
   public bool IsAuthenticated { get; private set; }
   /// <summary>
   /// Stores the currently logged-in user.
   /// </summary>
   public User? CurrentUser { get; private set; }
   /// <summary>
   /// Stores the roles assigned to the current user.
   /// </summary>
   public List<string> CurrentUserRoles { get; private set; } = new();
   /// <summary>
   /// Attempts to log in the user by sending credentials to the API.
   /// On success, stores the JWT token securely and updates authentication state.
   /// </summary>
   public async Task<AuthenticationResult> LoginAsync(string email, string password)
   {
       try
       {
           // Send login request to API
           var response = await _httpClient.PostAsJsonAsync("/auth/token", new
           {
               email,
               password
           });
           // Return error if login fails
           if (!response.IsSuccessStatusCode)
           {
               return new AuthenticationResult(false, "Invalid login");
           }
           // Read response JSON
           var json = await response.Content.ReadAsStringAsync();
           // Deserialize token response
           var result = JsonSerializer.Deserialize<TokenResponse>(json, new JsonSerializerOptions
           {
               PropertyNameCaseInsensitive = true
           });
           // Validate response
           if (result == null)
           {
               return new AuthenticationResult(false, "Invalid response");
           }
           // Store JWT token securely for future requests
           await SecureStorage.SetAsync("jwt_token", result.Token);
           // Create minimal user object (API does not return full user details)
           CurrentUser = new User
           {
               Id = result.UserId,
               Email = email,
               FirstName = "User",
               LastName = ""
           };
           // Assign default role
           CurrentUserRoles = new List<string> { "User" };
           // Update authentication state
           IsAuthenticated = true;
           AuthenticationStateChanged?.Invoke(this, true);
           return new AuthenticationResult(true, "Login successful");
       }
       catch (Exception ex)
       {
           // Handle unexpected errors
           return new AuthenticationResult(false, ex.Message);
       }
   }
   /// <summary>
   /// Placeholder method for user registration (not implemented).
   /// </summary>
   public Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password)
   {
       return Task.FromResult(new AuthenticationResult(false, "Register not implemented yet"));
   }
   /// <summary>
   /// Logs the user out by clearing the stored token and resetting authentication state.
   /// </summary>
   public async Task LogoutAsync()
   {
       await SecureStorage.SetAsync("jwt_token", "");
       IsAuthenticated = false;
       CurrentUser = null;
       // Notify subscribers that the user is logged out
       AuthenticationStateChanged?.Invoke(this, false);
   }
   /// <summary>
   /// Checks if the current user has a specific role.
   /// </summary>
   public bool HasRole(string roleName) => CurrentUserRoles.Contains(roleName);
   /// <summary>
   /// Checks if the current user has at least one of the specified roles.
   /// </summary>
   public bool HasAnyRole(params string[] roleNames) =>
       roleNames.Any(r => CurrentUserRoles.Contains(r));
   /// <summary>
   /// Checks if the current user has all of the specified roles.
   /// </summary>
   public bool HasAllRoles(params string[] roleNames) =>
       roleNames.All(r => CurrentUserRoles.Contains(r));
   /// <summary>
   /// Placeholder method for password change functionality (not implemented).
   /// </summary>
   public Task<bool> ChangePasswordAsync(string currentPassword, string newPassword)
   {
       return Task.FromResult(false);
   }
   /// <summary>
   /// Internal class representing the API response for authentication tokens.
   /// </summary>
   private class TokenResponse
   {
       public string Token { get; set; } = string.Empty;
       public DateTime ExpiresAt { get; set; }
       public int UserId { get; set; }
   }
}