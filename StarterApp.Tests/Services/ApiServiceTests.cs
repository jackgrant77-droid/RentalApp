using Xunit;
using StarterApp.Database.Models;
namespace StarterApp.Tests.Services;
public class ApiServiceTests
{
   [Fact]
   public void Item_ShouldStoreCorrectValues()
   {
       // Arrange
       var item = new Item
       {
           Id = 1,
           Title = "Test",
           Description = "Desc",
           DailyRate = 10
       };
       // Act
       var result = item;
       // Assert
       Assert.Equal("Test", result.Title);
       Assert.Equal(10, result.DailyRate);
   }
}