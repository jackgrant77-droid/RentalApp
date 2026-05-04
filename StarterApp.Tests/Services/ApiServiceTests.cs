using Xunit;

using StarterApp.Database.Models;

namespace StarterApp.Tests.Services;

/// <summary>

/// Basic test class for verifying Item model behaviour.

/// </summary>

public class ApiServiceTests

{

    /// <summary>

    /// Tests that an Item object correctly stores and returns its property values.

    /// </summary>

    [Fact]

    public void Item_ShouldStoreCorrectValues()

    {

        // Arrange: create a new Item with test values

        var item = new Item

        {

            Id = 1,

            Title = "Test",

            Description = "Desc",

            DailyRate = 10

        };

        // Act: assign the item to a variable (no transformation)

        var result = item;

        // Assert: verify values are stored correctly

        Assert.Equal("Test", result.Title);

        Assert.Equal(10, result.DailyRate);

    }

}
 