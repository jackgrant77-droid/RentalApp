using Xunit;

using StarterApp.Database.Models;

namespace StarterApp.Tests.Services;

public class ItemTests

{

    [Fact]

    public void Item_ShouldStoreCorrectValues()

    {

        // Arrange

        var item = new Item

        {

            Id = 1,

            Title = "Drill",

            Description = "Power drill",

            DailyRate = 5

        };

        // Act

        var title = item.Title;

        // Assert

        Assert.Equal("Drill", title);

    }

}
 