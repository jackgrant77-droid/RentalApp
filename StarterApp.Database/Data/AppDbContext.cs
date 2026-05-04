using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;

using StarterApp.Database.Models;

namespace StarterApp.Database.Data;

/// <summary>

/// Represents the application's database context using Entity Framework Core.

/// Handles database connections, entity configuration, and schema setup.

/// </summary>

public class AppDbContext : DbContext

{

    /// <summary>

    /// Default constructor.

    /// </summary>

    public AppDbContext()

    { }

    /// <summary>

    /// Constructor used when dependency injection provides options.

    /// </summary>

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)

    { }

    /// <summary>

    /// Configures the database connection.

    /// Uses an environment variable if available, otherwise falls back to appsettings.json.

    /// </summary>

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {

        if (optionsBuilder.IsConfigured) return;

        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        if (string.IsNullOrEmpty(connectionString))

        {

            // Load connection string from embedded appsettings.json

            var assembly = Assembly.GetExecutingAssembly();

            using var stream = assembly.GetManifestResourceStream("StarterApp.Database.appsettings.json");

            var config = new ConfigurationBuilder()

                .AddJsonStream(stream)

                .Build();

            connectionString = config.GetConnectionString("DevelopmentConnection");

        }

        // Configure PostgreSQL database provider

        optionsBuilder.UseNpgsql(connectionString);

    }

    /// <summary>

    /// Database tables mapped to application models.

    /// </summary>

    public DbSet<Role> Roles { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Rental> Rentals { get; set; }

    /// <summary>

    /// Configures entity relationships, constraints, and database schema.

    /// </summary>

    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {

        base.OnModelCreating(modelBuilder);

        // Configure User entity

        modelBuilder.Entity<User>(entity =>

        {

            entity.HasIndex(e => e.Email).IsUnique(); // Ensure unique email addresses

            entity.Property(e => e.Email).HasMaxLength(255);

            entity.Property(e => e.FirstName).HasMaxLength(100);

            entity.Property(e => e.LastName).HasMaxLength(100);

            entity.Property(e => e.PasswordHash).HasMaxLength(255);

            entity.Property(e => e.PasswordSalt).HasMaxLength(255);

        });

        // Configure Role entity

        modelBuilder.Entity<Role>(entity =>

        {

            entity.HasIndex(e => e.Name).IsUnique(); // Prevent duplicate role names

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.Property(e => e.Description).HasMaxLength(500);

        });

        // Configure UserRole (many-to-many relationship)

        modelBuilder.Entity<UserRole>(entity =>

        {

            entity.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique(); // Prevent duplicate role assignments

            entity.HasOne(ur => ur.User)

                  .WithMany(u => u.UserRoles)

                  .HasForeignKey(ur => ur.UserId);

            entity.HasOne(ur => ur.Role)

                  .WithMany(r => r.UserRoles)

                  .HasForeignKey(ur => ur.RoleId);

        });

    }

}
 