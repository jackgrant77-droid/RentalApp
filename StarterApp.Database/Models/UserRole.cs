using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StarterApp.Database.Models;
/// <summary>
/// Represents the many-to-many relationship between Users and Roles.
/// Includes metadata such as timestamps and soft delete support.
/// </summary>
[Table("user_role")]
[PrimaryKey(nameof(Id))]
public class UserRole
{
   /// <summary>
   /// Unique identifier for the UserRole record.
   /// </summary>
   public int Id { get; set; }
   /// <summary>
   /// Foreign key referencing the User.
   /// </summary>
   [Required]
   public int UserId { get; set; }
   /// <summary>
   /// Foreign key referencing the Role.
   /// </summary>
   [Required]
   public int RoleId { get; set; }
   /// <summary>
   /// Navigation property to the associated User.
   /// </summary>
   [ForeignKey(nameof(UserId))]
   public User User { get; set; } = null!;
   /// <summary>
   /// Navigation property to the associated Role.
   /// </summary>
   [ForeignKey(nameof(RoleId))]
   public Role Role { get; set; } = null!;
   /// <summary>
   /// Timestamp when the relationship was created.
   /// </summary>
   public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
   /// <summary>
   /// Timestamp when the relationship was last updated.
   /// </summary>
   public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
   /// <summary>
   /// Timestamp for soft deletion.
   /// </summary>
   public DateTime? DeletedAt { get; set; }
   /// <summary>
   /// Indicates whether this relationship is active.
   /// </summary>
   public bool IsActive { get; set; } = true;
   /// <summary>
   /// Default constructor.
   /// </summary>
   public UserRole()
   {
       CreatedAt = DateTime.UtcNow;
       UpdatedAt = DateTime.UtcNow;
       IsActive = true;
   }
   /// <summary>
   /// Constructor to create a new UserRole relationship.
   /// </summary>
   public UserRole(int userId, int roleId)
   {
       UserId = userId;
       RoleId = roleId;
       CreatedAt = DateTime.UtcNow;
       UpdatedAt = DateTime.UtcNow;
       IsActive = true;
   }
   /// <summary>
   /// Updates the last modified timestamp.
   /// </summary>
   public void UpdateTimestamps()
   {
       UpdatedAt = DateTime.UtcNow;
   }
   /// <summary>
   /// Marks the relationship as deleted (soft delete).
   /// </summary>
   public void MarkAsDeleted()
   {
       DeletedAt = DateTime.UtcNow;
       IsActive = false;
   }
   /// <summary>
   /// Restores a previously deleted relationship.
   /// </summary>
   public void Restore()
   {
       DeletedAt = null;
       IsActive = true;
       UpdatedAt = DateTime.UtcNow;
   }
   /// <summary>
   /// Returns a string representation of the UserRole.
   /// </summary>
   public override string ToString()
   {
       return $"UserRole(Id: {Id}, UserId: {UserId}, RoleId: {RoleId}, CreatedAt: {CreatedAt}, UpdatedAt: {UpdatedAt}, DeletedAt: {DeletedAt}, IsActive: {IsActive})";
   }
   /// <summary>
   /// Compares two UserRole objects for equality.
   /// </summary>
   public override bool Equals(object? obj)
   {
       if (obj is UserRole other)
       {
           return Id == other.Id &&
                  UserId == other.UserId &&
                  RoleId == other.RoleId &&
                  CreatedAt == other.CreatedAt &&
                  UpdatedAt == other.UpdatedAt &&
                  DeletedAt == other.DeletedAt &&
                  IsActive == other.IsActive;
       }
       return false;
   }
   /// <summary>
   /// Generates a hash code for the UserRole.
   /// </summary>
   public override int GetHashCode()
   {
       return HashCode.Combine(Id, UserId, RoleId, CreatedAt, UpdatedAt, DeletedAt, IsActive);
   }
}