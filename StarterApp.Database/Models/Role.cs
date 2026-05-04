using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StarterApp.Database.Models;
/// <summary>
/// Represents a user role within the system (e.g., Admin, User).
/// Roles are used to control permissions and access.
/// </summary>
[Table("role")]
[PrimaryKey(nameof(Id))]
public class Role
{
   /// <summary>
   /// Unique identifier for the role.
   /// </summary>
   public int Id { get; set; }
   /// <summary>
   /// Name of the role (must be unique).
   /// </summary>
   [Required]
   public string Name { get; set; } = string.Empty;
   /// <summary>
   /// Description of the role's purpose.
   /// </summary>
   [Required]
   public string Description { get; set; } = string.Empty;
   /// <summary>
   /// Indicates whether this role is assigned by default to new users.
   /// </summary>
   public bool IsDefault { get; set; } = false;
   /// <summary>
   /// Navigation property linking users to roles (many-to-many relationship).
   /// </summary>
   public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
}