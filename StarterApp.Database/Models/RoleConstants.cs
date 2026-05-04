namespace StarterApp.Database.Models;

/// <summary>

/// Defines constant role names used throughout the application.

/// Helps avoid hardcoding strings and ensures consistency.

/// </summary>

public static class RoleConstants

{

    /// <summary>

    /// Administrator role with full access permissions.

    /// </summary>

    public const string Admin = "Admin";

    /// <summary>

    /// Standard user role with basic access.

    /// </summary>

    public const string OrdinaryUser = "OrdinaryUser";

    /// <summary>

    /// Special user role with additional privileges.

    /// </summary>

    public const string SpecialUser = "SpecialUser";

    /// <summary>

    /// Collection of all defined roles.

    /// Useful for validation or role assignment.

    /// </summary>

    public static readonly string[] AllRoles = { Admin, OrdinaryUser, SpecialUser };

}
 