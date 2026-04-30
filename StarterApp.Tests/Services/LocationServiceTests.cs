using Xunit;
using Moq;
namespace StarterApp.Tests.Services;
public interface ILocationService
{
   Task<Location> GetCurrentLocationAsync();
}
public class Location
{
   public double Latitude { get; set; }
   public double Longitude { get; set; }
}
public class LocationServiceTests
{
   [Fact]
   public async Task GetCurrentLocationAsync_ReturnsMockedLocation()
   {
       // Arrange
       var mock = new Mock<ILocationService>();
       mock.Setup(x => x.GetCurrentLocationAsync())
           .ReturnsAsync(new Location
           {
               Latitude = 55.9533,
               Longitude = -3.1883
           });
       // Act
       var result = await mock.Object.GetCurrentLocationAsync();
       // Assert
       Assert.Equal(55.9533, result.Latitude);
   }
}