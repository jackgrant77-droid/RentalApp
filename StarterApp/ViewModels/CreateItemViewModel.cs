using System.Windows.Input;
using StarterApp.Database.Data.Repositories;
using StarterApp.Database.Models;

namespace StarterApp.ViewModels;

public class CreateItemViewModel
{
    private readonly IItemRepository _itemRepository;

    public string Title { get; set; }
    public string Description { get; set; }
    public decimal DailyRate { get; set; }
    public string Category { get; set; }

    public ICommand SaveCommand { get; }

    public CreateItemViewModel(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
        SaveCommand = new Command(async () => await SaveItemAsync());
    }

    private async Task SaveItemAsync()
    {
        var item = new Item
        {
            Title = Title,
            Description = Description,
            DailyRate = DailyRate,
            Category = Category
        };

        await _itemRepository.AddAsync(item);

        await Application.Current.MainPage.DisplayAlert("Success", "Item created!", "OK");
    }
}