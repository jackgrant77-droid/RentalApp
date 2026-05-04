using StarterApp.Database.Models;

using StarterApp.ViewModels;

namespace StarterApp.Views;

/// <summary>

/// Page that displays a list of items and allows users to create items

/// or request rentals. Binds to ItemsListViewModel.

/// </summary>

public partial class ItemsListPage : ContentPage

{

    private readonly ItemsListViewModel _viewModel;

    private readonly CreateItemPage _createItemPage;

    private readonly RequestRentalPage _requestRentalPage;

    /// <summary>

    /// Initializes the page and injects required ViewModels and Pages.

    /// Sets the binding context for MVVM data binding.

    /// </summary>

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

    /// <summary>

    /// Called when the page appears.

    /// Triggers loading of items from the ViewModel.

    /// </summary>

    protected override void OnAppearing()

    {

        base.OnAppearing();

        _viewModel.LoadItemsCommand.Execute(null);

    }

    /// <summary>

    /// Navigates to the Create Item page when the user clicks the button.

    /// </summary>

    private async void OnCreateItemClicked(object sender, EventArgs e)

    {

        await Navigation.PushAsync(_createItemPage);

    }

    /// <summary>

    /// Handles rental request button click.

    /// Sets the selected item's ID in the RequestRentalViewModel

    /// and navigates to the rental request page.

    /// </summary>

    private async void OnRequestRentalClicked(object sender, EventArgs e)

    {

        if (sender is Button button && button.CommandParameter is Item item)

        {

            var viewModel = _requestRentalPage.BindingContext as RequestRentalViewModel;

            if (viewModel is not null)

            {

                // Pass selected item ID to the next page

                viewModel.ItemId = item.Id;

            }

            await Navigation.PushAsync(_requestRentalPage);

        }

    }

}
 