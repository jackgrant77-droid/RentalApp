using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class MainPage : ContentPage
{
    private readonly ItemsListPage _itemsListPage;
    private readonly RentalsPage _rentalsPage;

    private readonly NearbyItemsPage _nearbyItemsPage;

    public MainPage(
        MainViewModel viewModel,
        ItemsListPage itemsListPage,
        RentalsPage rentalsPage,
        NearbyItemsPage nearbyItemsPage)
        
    {
        InitializeComponent();
        BindingContext = viewModel;

        _itemsListPage = itemsListPage;
        _rentalsPage = rentalsPage;
        _nearbyItemsPage = nearbyItemsPage;
    }

    private async void OnViewItemsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_itemsListPage);
    }

    private async void OnViewRentalsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_rentalsPage);
    }

    private async void OnFindNearbyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_nearbyItemsPage);
    }
}