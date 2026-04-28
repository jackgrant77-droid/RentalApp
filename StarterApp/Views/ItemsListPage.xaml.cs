using StarterApp.Database.Models;
using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class ItemsListPage : ContentPage
{
    private readonly ItemsListViewModel _viewModel;
    private readonly CreateItemPage _createItemPage;
    private readonly RequestRentalPage _requestRentalPage;

    public ItemsListPage(
        ItemsListViewModel viewModel,
        CreateItemPage createItemPage,
        RequestRentalPage requestRentalPage)
    {
        InitializeComponent();
        BindingContext = viewModel;

        _viewModel = viewModel;
        _createItemPage = createItemPage;
        _requestRentalPage = requestRentalPage;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadItemsCommand.Execute(null);
    }

    private async void OnCreateItemClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_createItemPage);
    }

    private async void OnRequestRentalClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Item item)
        {
            var viewModel = _requestRentalPage.BindingContext as RequestRentalViewModel;

            if (viewModel is not null)
            {
                viewModel.ItemId = item.Id;
                
            }

            await Navigation.PushAsync(_requestRentalPage);
        }
    }
}