using System.Collections.ObjectModel;
using System.Windows.Input;
using StarterApp.Database.Data.Repositories;
using StarterApp.Database.Models;

namespace StarterApp.ViewModels;

public class ItemsListViewModel
{
    private readonly IItemRepository _itemRepository;

    public ObservableCollection<Item> Items { get; } = new();

    public ICommand LoadItemsCommand { get; }

    public ItemsListViewModel(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
        LoadItemsCommand = new Command(async () => await LoadItemsAsync());
    }

    private async Task LoadItemsAsync()
    {
        Items.Clear();

        var items = await _itemRepository.GetAllAsync();

        foreach (var item in items)
        {
            Items.Add(item);
        }
    }
}