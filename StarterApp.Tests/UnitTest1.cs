using Xunit;

namespace StarterApp.Tests;

/// <summary>

/// Basic test class to ensure the test project is functioning correctly.

/// </summary>

public class UnitTest1

{

    /// <summary>

    /// Simple test to verify that the test framework runs successfully.

    /// </summary>

    [Fact]

    public void Test1_ShouldPass()

    {

        // Arrange

        var expected = true;

        // Act

        var actual = true;

        // Assert

        Assert.Equal(expected, actual);

    }

}
 
