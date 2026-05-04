using System.Collections.ObjectModel;

using System.Windows.Input;

using StarterApp.Database.Models;

using StarterApp.Services;

namespace StarterApp.ViewModels;

/// <summary>

/// ViewModel responsible for loading and managing a list of items.

/// Binds to the UI and retrieves data from the API service.

/// </summary>

public class ItemsListViewModel

{

    private readonly IApiService _apiService;

    /// <summary>

    /// Collection of items displayed in the UI.

    /// Uses ObservableCollection to automatically update the UI when data changes.

    /// </summary>

    public ObservableCollection<Item> Items { get; } = new();

    /// <summary>

    /// Command used to trigger loading of items.

    /// Bound to UI elements such as buttons or page load events.

    /// </summary>

    public ICommand LoadItemsCommand { get; }

    public ItemsListViewModel(IApiService apiService)

    {

        _apiService = apiService;

        // Bind command to LoadItemsAsync method

        LoadItemsCommand = new Command(async () => await LoadItemsAsync());

    }

    /// <summary>

    /// Loads items from the API and updates the observable collection.

    /// Handles errors and displays feedback to the user.

    /// </summary>

    public async Task LoadItemsAsync()

    {

        try

        {

            // Clear existing items before reloading

            Items.Clear();

            // Retrieve items from API

            var items = await _apiService.GetItemsAsync();

            // Populate observable collection

            foreach (var item in items)

            {

                Items.Add(item);

            }

        }

        catch (Exception ex)

        {

            // Display error message if loading fails

            await Application.Current.MainPage.DisplayAlert(

                "Error",

                $"Could not load items: {ex.Message}",

                "OK");

        }

    }

}
 