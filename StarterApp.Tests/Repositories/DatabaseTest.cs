using Xunit;

using StarterApp.Tests.Fixtures;

using StarterApp.Database.Models;

namespace StarterApp.Tests.Repositories;

/// <summary>

/// Unit tests for database operations using an in-memory database.

/// Verifies that data can be added and retrieved correctly.

/// </summary>

public class DatabaseTests : IClassFixture<DatabaseFixture>

{

    private readonly DatabaseFixture _fixture;

    /// <summary>

    /// Initializes the test class with a shared database fixture.

    /// </summary>

    public DatabaseTests(DatabaseFixture fixture)

    {

        _fixture = fixture;

    }

    /// <summary>

    /// Tests that an item can be added to the database

    /// and retrieved successfully.

    /// </summary>

    [Fact]

    public void Database_ShouldAddAndReturnItem()

    {

        // Arrange: create test data

        var context = _fixture.Context;

        var item = new Item

        {

            Title = "Test Item",

            Description = "Test description",

            DailyRate = 10

        };

        // Act: add item and retrieve from database

        context.Items.Add(item);

        context.SaveChanges();

        var items = context.Items.ToList();

        // Assert: verify item was added correctly

        Assert.Single(items);

        Assert.Equal("Test Item", items[0].Title);

    }

}
 