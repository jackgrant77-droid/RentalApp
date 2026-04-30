using Moq;

using Xunit;

namespace StarterApp.Tests.Services;

public interface ILocationService

{

    Task<TestLocation> GetCurrentLocationAsync();

}

public class TestLocation

{

    public double Latitude { get; set; }

    public double Longitude { get; set; }

}

public class LocationServiceTests

{

    [Fact]

    public async Task GetCurrentLocationAsync_WhenMocked_ReturnsExpectedLocation()

    {

        // Arrange

        var mockLocationService = new Mock<ILocationService>();

        mockLocationService.Setup(service => service.GetCurrentLocationAsync())

            .ReturnsAsync(new TestLocation

            {

                Latitude = 55.9533,

                Longitude = -3.1883

            });

        // Act

        var result = await mockLocationService.Object.GetCurrentLocationAsync();

        // Assert

        Assert.NotNull(result);

        Assert.Equal(55.9533, result.Latitude);

        Assert.Equal(-3.1883, result.Longitude);

    }

}
 