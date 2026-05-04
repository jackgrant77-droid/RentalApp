using System.Windows.Input;
using StarterApp.Database.Models;
using StarterApp.Services;
namespace StarterApp.ViewModels;
/// <summary>
/// ViewModel responsible for handling item creation logic.
/// Binds to the UI and sends new item data to the API.
/// </summary>
public class CreateItemViewModel
{
   private readonly IApiService _apiService;
   /// <summary>
   /// Item title entered by the user.
   /// </summary>
   public string Title { get; set; } = string.Empty;
   /// <summary>
   /// Item description entered by the user.
   /// </summary>
   public string Description { get; set; } = string.Empty;
   /// <summary>
   /// Daily rental price for the item.
   /// </summary>
   public decimal DailyRate { get; set; }
   /// <summary>
   /// Category assigned to the item.
   /// </summary>
   public string Category { get; set; } = string.Empty;
   /// <summary>
   /// Command triggered when the user saves a new item.
   /// </summary>
   public ICommand SaveCommand { get; }
   public CreateItemViewModel(IApiService apiService)
   {
       _apiService = apiService;
       // Bind save button to SaveAsync method
       SaveCommand = new Command(async () => await SaveAsync());
   }
   /// <summary>
   /// Creates a new item and sends it to the API.
   /// Displays success or error messages to the user.
   /// </summary>
   private async Task SaveAsync()
   {
       try
       {
           // Create item object from user input
           var item = new Item
           {
               Title = Title,
               Description = Description,
               DailyRate = DailyRate,
               Category = Category
           };
           // Send item to API
           await _apiService.CreateItemAsync(item);
           // Notify user of success
           await Application.Current.MainPage.DisplayAlert(
               "Success",
               "Item created via API!",
               "OK");
       }
       catch (Exception ex)
       {
           // Handle errors and notify user
           await Application.Current.MainPage.DisplayAlert(
               "Error",
               ex.Message,
               "OK");
       }
   }
}