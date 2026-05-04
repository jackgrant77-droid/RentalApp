using StarterApp.Database.Data;

using Microsoft.EntityFrameworkCore;

namespace StarterApp.Tests.Fixtures;

/// <summary>

/// Provides a shared in-memory database context for unit tests.

/// Ensures tests run in isolation without affecting the real database.

/// </summary>

public class DatabaseFixture

{

    /// <summary>

    /// The in-memory database context used for testing.

    /// </summary>

    public AppDbContext Context { get; }

    /// <summary>

    /// Initializes the in-memory database for test execution.

    /// </summary>

    public DatabaseFixture()

    {

        // Configure EF Core to use an in-memory database

        var options = new DbContextOptionsBuilder<AppDbContext>()

            .UseInMemoryDatabase(databaseName: "TestDatabase")

            .Options;

        // Create database context with test configuration

        Context = new AppDbContext(options);

    }

}
 