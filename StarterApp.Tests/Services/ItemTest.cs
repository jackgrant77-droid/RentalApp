using Xunit;
using StarterApp.Database.Models;
namespace StarterApp.Tests.Services;
/// <summary>
/// Unit tests for the Item model.
/// Verifies that properties are correctly stored and retrieved.
/// </summary>
public class ItemTests
{
   /// <summary>
   /// Tests that an Item correctly stores and returns its Title property.
   /// </summary>
   [Fact]
   public void Item_ShouldStoreCorrectValues()
   {
       // Arrange: create a test item with sample data
       var item = new Item
       {
           Id = 1,
           Title = "Drill",
           Description = "Power drill",
           DailyRate = 5
       };
       // Act: access the Title property
       var title = item.Title;
       // Assert: verify the title is stored correctly
       Assert.Equal("Drill", title);
   }
}