using Xunit;
using StarterApp.Tests.Fixtures;
using StarterApp.Database.Models;
using StarterApp.Database.Data.Repositories;
namespace StarterApp.Tests.Repositories;
/// <summary>
/// Tests for ItemRepository using an in-memory database.
/// </summary>
public class ItemRepositoryTests : IClassFixture<DatabaseFixture>
{
   private readonly DatabaseFixture _fixture;
   public ItemRepositoryTests(DatabaseFixture fixture)
   {
       _fixture = fixture;
   }
   [Fact]
   public async Task AddAsync_WhenItemIsValid_AddsItemToDatabase()
   {
       // Arrange
       var repository = new ItemRepository(_fixture.Context);
       var item = new Item
       {
           Title = "Test Drill",
           Description = "Cordless drill",
           DailyRate = 5,
           Category = "Tools"
       };
       // Act
       await repository.AddAsync(item);
       var items = await repository.GetAllAsync();
       // Assert
       Assert.Contains(items, i => i.Title == "Test Drill");
   }
   [Fact]
   public async Task GetByIdAsync_WhenItemExists_ReturnsCorrectItem()
   {
       // Arrange
       var repository = new ItemRepository(_fixture.Context);
       var item = new Item
       {
           Title = "Test Tent",
           Description = "Camping tent",
           DailyRate = 12,
           Category = "Camping"
       };
       await repository.AddAsync(item);
       // Act
       var result = await repository.GetByIdAsync(item.Id);
       // Assert
       Assert.NotNull(result);
       Assert.Equal("Test Tent", result.Title);
   }
   [Fact]
   public async Task DeleteAsync_WhenItemExists_RemovesItemFromDatabase()
   {
       // Arrange
       var repository = new ItemRepository(_fixture.Context);
       var item = new Item
       {
           Title = "Test Bike",
           Description = "Mountain bike",
           DailyRate = 15,
           Category = "Sports"
       };
       await repository.AddAsync(item);
       // Act
       await repository.DeleteAsync(item.Id);
       var result = await repository.GetByIdAsync(item.Id);
       // Assert
       Assert.Null(result);
   }
}