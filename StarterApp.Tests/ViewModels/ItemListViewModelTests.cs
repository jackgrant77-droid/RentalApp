using Xunit;

using Moq;

using StarterApp.Services;

using StarterApp.ViewModels;

using StarterApp.Database.Models;

namespace StarterApp.Tests.ViewModels;

public class ItemsListViewModelTests

{

    [Fact]

    public async Task LoadItemsAsync_WhenApiReturnsItems_PopulatesItemsCollection()

    {

        // Arrange

        var mockApi = new Mock<IApiService>();

        mockApi.Setup(x => x.GetItemsAsync())

            .ReturnsAsync(new List<Item>

            {

                new Item { Id = 1, Title = "Item 1" },

                new Item { Id = 2, Title = "Item 2" }

            });

        var viewModel = new ItemsListViewModel(mockApi.Object);

        // Act

        await viewModel.LoadItemsAsync();

        // Assert

        Assert.Equal(2, viewModel.Items.Count);

    }

}
 