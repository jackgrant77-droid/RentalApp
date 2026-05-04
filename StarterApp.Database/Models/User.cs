using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace StarterApp.Database.Models;

/// <summary>

/// Represents a user within the system.

/// Stores authentication details, personal information, and role relationships.

/// </summary>

[Table("users")]

[PrimaryKey(nameof(Id))]

public class User

{

    /// <summary>

    /// Unique identifier for the user.

    /// </summary>

    public int Id { get; set; }

    /// <summary>

    /// User's first name.

    /// </summary>

    [Required]

    public string FirstName { get; set; } = string.Empty;

    /// <summary>

    /// User's last name.

    /// </summary>

    [Required]

    public string LastName { get; set; } = string.Empty;

    /// <summary>

    /// User's email address (used for login).

    /// </summary>

    [Required]

    public string Email { get; set; } = string.Empty;

    /// <summary>

    /// Hashed password for secure authentication.

    /// </summary>

    [Required]

    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>

    /// Salt used for password hashing.

    /// </summary>

    [Required]

    public string PasswordSalt { get; set; } = string.Empty;

    /// <summary>

    /// Timestamp when the user account was created.

    /// </summary>

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>

    /// Timestamp when the user account was last updated.

    /// </summary>

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>

    /// Timestamp when the user account was deleted (soft delete).

    /// </summary>

    public DateTime? DeletedAt { get; set; }

    /// <summary>

    /// Indicates whether the user account is active.

    /// </summary>

    public bool IsActive { get; set; } = true;

    /// <summary>

    /// Navigation property linking users to roles (many-to-many relationship).

    /// </summary>

    public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

    /// <summary>

    /// Computed property that returns the user's full name.

    /// Not mapped to the database.

    /// </summary>

    [NotMapped]

    public string FullName => $"{FirstName} {LastName}";

}
 