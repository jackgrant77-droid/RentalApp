using System.Collections.ObjectModel;
using System.Windows.Input;
using StarterApp.Database.Models;
using StarterApp.Services;

namespace StarterApp.ViewModels;

public class ItemsListViewModel
{
    private readonly IApiService _apiService;

    public ObservableCollection<Item> Items { get; } = new();

    public ICommand LoadItemsCommand { get; }

    public ItemsListViewModel(IApiService apiService)
    {
        _apiService = apiService;
        LoadItemsCommand = new Command(async () => await LoadItemsAsync());
    }

    private async Task LoadItemsAsync()
    {
        try
        {
            Items.Clear();

            var items = await _apiService.GetItemsAsync();

            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                $"Could not load items: {ex.Message}",
                "OK");
        }
    }
}