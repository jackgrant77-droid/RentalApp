using System.Windows.Input;
using StarterApp.Services;
namespace StarterApp.ViewModels;
/// <summary>
/// ViewModel responsible for creating rental requests for selected items.
/// Handles rental date selection and communicates with the API service.
/// </summary>
public class RequestRentalViewModel
{
   private readonly IApiService _apiService;
   /// <summary>
   /// The ID of the item being requested for rental.
   /// </summary>
   public int ItemId { get; set; }
   /// <summary>
   /// Start date of the rental period.
   /// Defaults to tomorrow.
   /// </summary>
   public DateTime StartDate { get; set; } = DateTime.Today.AddDays(1);
   /// <summary>
   /// End date of the rental period.
   /// Defaults to two days from today.
   /// </summary>
   public DateTime EndDate { get; set; } = DateTime.Today.AddDays(2);
   /// <summary>
   /// Command triggered when the user submits a rental request.
   /// </summary>
   public ICommand RequestRentalCommand { get; }
   public RequestRentalViewModel(IApiService apiService)
   {
       _apiService = apiService;
       // Bind request button to rental request logic
       RequestRentalCommand = new Command(async () => await RequestRentalAsync());
   }
   /// <summary>
   /// Sends a rental request to the API and displays success or error feedback.
   /// </summary>
   private async Task RequestRentalAsync()
   {
       try
       {
           // Submit rental request using selected item and dates
           await _apiService.RequestRentalAsync(ItemId, StartDate, EndDate);
           await Application.Current.MainPage.DisplayAlert(
               "Success",
               "Rental request submitted!",
               "OK");
       }
       catch (Exception ex)
       {
           // Display API validation or request errors
           await Application.Current.MainPage.DisplayAlert(
               "Error",
               ex.Message,
               "OK");
       }
   }
}