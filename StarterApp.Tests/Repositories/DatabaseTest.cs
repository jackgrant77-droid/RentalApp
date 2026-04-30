using Xunit;

using StarterApp.Tests.Fixtures;

namespace StarterApp.Tests.Repositories;

public class DatabaseTests : IClassFixture<DatabaseFixture>

{

    private readonly DatabaseFixture _fixture;

    public DatabaseTests(DatabaseFixture fixture)

    {

        _fixture = fixture;

    }

    [Fact]

    public void Database_ShouldContainSeededItem()

    {

        // Arrange

        var context = _fixture.Context;

        // Act

        var items = context.Items.ToList();

        // Assert

        Assert.Single(items);

        Assert.Equal("Test Item", items[0].Title);

    }

}
 