using Microsoft.EntityFrameworkCore;
using StarterApp.Database.Data;
using StarterApp.Database.Models;
using BCrypt.Net;
namespace StarterApp.Services;
/// <summary>
/// Handles local database authentication, registration, logout, roles,
/// and password management using Entity Framework Core and BCrypt.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
   private readonly AppDbContext _context;
   private User? _currentUser;
   private List<string> _currentUserRoles = new();
   /// <summary>
   /// Event raised when the user logs in or logs out.
   /// </summary>
   public event EventHandler<bool>? AuthenticationStateChanged;
   public AuthenticationService(AppDbContext context)
   {
       _context = context;
   }
   /// <summary>
   /// Indicates whether a user is currently logged in.
   /// </summary>
   public bool IsAuthenticated => _currentUser != null;
   /// <summary>
   /// Stores the currently authenticated user.
   /// </summary>
   public User? CurrentUser => _currentUser;
   /// <summary>
   /// Stores the roles assigned to the currently authenticated user.
   /// </summary>
   public List<string> CurrentUserRoles => _currentUserRoles;
   /// <summary>
   /// Attempts to log in a user using their email and password.
   /// The password is verified against the stored BCrypt hash.
   /// </summary>
   public async Task<AuthenticationResult> LoginAsync(string email, string password)
   {
       try
       {
           var user = await _context.Users
               .Include(u => u.UserRoles)
               .ThenInclude(ur => ur.Role)
               .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
           if (user == null)
           {
               return new AuthenticationResult(false, "Invalid email or password");
           }
           if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
           {
               return new AuthenticationResult(false, "Invalid email or password");
           }
           _currentUser = user;
           _currentUserRoles = user.UserRoles
               .Where(ur => ur.IsActive)
               .Select(ur => ur.Role.Name)
               .ToList();
           AuthenticationStateChanged?.Invoke(this, true);
           return new AuthenticationResult(true, "Login successful");
       }
       catch (Exception ex)
       {
           return new AuthenticationResult(false, $"Login failed: {ex.Message}");
       }
   }
   /// <summary>
   /// Registers a new user in the local database and assigns the default role.
   /// Passwords are stored securely using BCrypt hashing.
   /// </summary>
   public async Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password)
   {
       try
       {
           var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
           if (existingUser != null)
           {
               return new AuthenticationResult(false, "User with this email already exists");
           }
           var salt = BCrypt.Net.BCrypt.GenerateSalt();
           var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
           var user = new User
           {
               FirstName = firstName,
               LastName = lastName,
               Email = email,
               PasswordHash = hashedPassword,
               PasswordSalt = salt,
               CreatedAt = DateTime.UtcNow,
               UpdatedAt = DateTime.UtcNow,
               IsActive = true
           };
           _context.Users.Add(user);
           await _context.SaveChangesAsync();
           var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.IsDefault == true);
           if (userRole != null)
           {
               var userRoleAssignment = new UserRole(user.Id, userRole.Id);
               _context.UserRoles.Add(userRoleAssignment);
               await _context.SaveChangesAsync();
           }
           return new AuthenticationResult(true, "Registration successful");
       }
       catch (Exception ex)
       {
           return new AuthenticationResult(false, $"Registration failed: {ex.Message}");
       }
   }
   /// <summary>
   /// Logs out the current user and clears stored role information.
   /// </summary>
   public Task LogoutAsync()
   {
       _currentUser = null;
       _currentUserRoles.Clear();
       AuthenticationStateChanged?.Invoke(this, false);
       return Task.CompletedTask;
   }
   /// <summary>
   /// Checks whether the current user has a specific role.
   /// </summary>
   public bool HasRole(string roleName)
   {
       return _currentUserRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase);
   }
   /// <summary>
   /// Checks whether the current user has at least one of the specified roles.
   /// </summary>
   public bool HasAnyRole(params string[] roleNames)
   {
       return roleNames.Any(role => HasRole(role));
   }
   /// <summary>
   /// Checks whether the current user has all of the specified roles.
   /// </summary>
   public bool HasAllRoles(params string[] roleNames)
   {
       return roleNames.All(role => HasRole(role));
   }
   /// <summary>
   /// Changes the current user's password after verifying their existing password.
   /// </summary>
   public async Task<bool> ChangePasswordAsync(string currentPassword, string newPassword)
   {
       if (_currentUser == null)
       {
           return false;
       }
       try
       {
           if (!BCrypt.Net.BCrypt.Verify(currentPassword, _currentUser.PasswordHash))
           {
               return false;
           }
           var salt = BCrypt.Net.BCrypt.GenerateSalt();
           var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, salt);
           _currentUser.PasswordHash = hashedPassword;
           _currentUser.PasswordSalt = salt;
           _currentUser.UpdatedAt = DateTime.UtcNow;
           _context.Users.Update(_currentUser);
           await _context.SaveChangesAsync();
           return true;
       }
       catch
       {
           return false;
       }
   }
}
/// <summary>
/// Represents the result of an authentication action such as login or registration.
/// </summary>
public class AuthenticationResult
{
   public bool IsSuccess { get; }
   public string Message { get; }
   public AuthenticationResult(bool isSuccess, string message)
   {
       IsSuccess = isSuccess;
       Message = message;
   }
}