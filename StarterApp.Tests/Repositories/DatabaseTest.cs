using Xunit;
using StarterApp.Tests.Fixtures;
using StarterApp.Database.Models;
namespace StarterApp.Tests.Repositories;
public class DatabaseTests : IClassFixture<DatabaseFixture>
{
   private readonly DatabaseFixture _fixture;
   public DatabaseTests(DatabaseFixture fixture)
   {
       _fixture = fixture;
   }
   [Fact]
   public void Database_ShouldAddAndReturnItem()
   {
       // Arrange
       var context = _fixture.Context;
       var item = new Item
       {
           Title = "Test Item",
           Description = "Test description",
           DailyRate = 10
       };
       // Act
       context.Items.Add(item);
       context.SaveChanges();
       var items = context.Items.ToList();
       // Assert
       Assert.Single(items);
       Assert.Equal("Test Item", items[0].Title);
   }
}