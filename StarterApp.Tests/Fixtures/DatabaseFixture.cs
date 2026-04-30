using StarterApp.Database.Data;
using Microsoft.EntityFrameworkCore;
namespace StarterApp.Tests.Fixtures;
public class DatabaseFixture
{
   public AppDbContext Context { get; }
   public DatabaseFixture()
   {
       var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;
       Context = new AppDbContext(options);
   }
}