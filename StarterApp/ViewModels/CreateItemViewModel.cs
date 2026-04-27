using System.Windows.Input;
using StarterApp.Database.Models;
using StarterApp.Services;

namespace StarterApp.ViewModels;

public class CreateItemViewModel
{
    private readonly IApiService _apiService;

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal DailyRate { get; set; }
    public string Category { get; set; } = string.Empty;

    public ICommand SaveCommand { get; }

    public CreateItemViewModel(IApiService apiService)
    {
        _apiService = apiService;
        SaveCommand = new Command(async () => await SaveAsync());
    }

    private async Task SaveAsync()
    {
        try
        {
            var item = new Item
            {
                Title = Title,
                Description = Description,
                DailyRate = DailyRate,
                Category = Category
            };

            await _apiService.CreateItemAsync(item);

            await Application.Current.MainPage.DisplayAlert(
                "Success",
                "Item created via API!",
                "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
    }
}