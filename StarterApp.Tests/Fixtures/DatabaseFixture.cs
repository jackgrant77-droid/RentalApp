using StarterApp.Database.Data;
using Microsoft.EntityFrameworkCore;
namespace StarterApp.Tests.Fixtures;
public class DatabaseFixture
{
   public AppDbContext Context { get; }
   public DatabaseFixture()
   {
       var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase("TestDatabase")
           .Options;
       Context = new AppDbContext(options);
       SeedData();
   }
   private void SeedData()
   {
       Context.Items.Add(new StarterApp.Database.Models.Item
       {
           Id = 1,
           Title = "Test Item",
           Description = "Test",
           DailyRate = 10
       });
       Context.SaveChanges();
   }
}