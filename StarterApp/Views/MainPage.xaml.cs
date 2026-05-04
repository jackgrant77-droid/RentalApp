using StarterApp.ViewModels;

namespace StarterApp.Views;

/// <summary>

/// Main landing page of the application.

/// Provides navigation to key features such as items, rentals, and nearby search.

/// </summary>

public partial class MainPage : ContentPage

{

    private readonly ItemsListPage _itemsListPage;

    private readonly RentalsPage _rentalsPage;

    private readonly NearbyItemsPage _nearbyItemsPage;

    /// <summary>

    /// Initializes the main page and injects required ViewModels and Pages.

    /// Sets the binding context for MVVM data binding.

    /// </summary>

    public MainPage(

        MainViewModel viewModel,

        ItemsListPage itemsListPage,

        RentalsPage rentalsPage,

        NearbyItemsPage nearbyItemsPage)

    {

        InitializeComponent();

        // Bind ViewModel to UI

        BindingContext = viewModel;

        _itemsListPage = itemsListPage;

        _rentalsPage = rentalsPage;

        _nearbyItemsPage = nearbyItemsPage;

    }

    /// <summary>

    /// Navigates to the Items list page.

    /// </summary>

    private async void OnViewItemsClicked(object sender, EventArgs e)

    {

        await Navigation.PushAsync(_itemsListPage);

    }

    /// <summary>

    /// Navigates to the Rentals page.

    /// </summary>

    private async void OnViewRentalsClicked(object sender, EventArgs e)

    {

        await Navigation.PushAsync(_rentalsPage);

    }

    /// <summary>

    /// Navigates to the Nearby Items page.

    /// </summary>

    private async void OnFindNearbyClicked(object sender, EventArgs e)

    {

        await Navigation.PushAsync(_nearbyItemsPage);

    }

}
 