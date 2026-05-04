using System.Collections.ObjectModel;
using System.Windows.Input;
using StarterApp.Database.Models;
using StarterApp.Services;
namespace StarterApp.ViewModels;
/// <summary>
/// ViewModel responsible for loading rentals and handling rental workflow actions.
/// Supports status changes such as approving, rejecting, returning, and completing rentals.
/// </summary>
public class RentalsViewModel
{
   private readonly IApiService _apiService;
   /// <summary>
   /// Collection of rentals displayed in the UI.
   /// ObservableCollection updates the UI automatically when data changes.
   /// </summary>
   public ObservableCollection<Rental> Rentals { get; } = new();
   /// <summary>
   /// Command used to load rentals from the API.
   /// </summary>
   public ICommand LoadRentalsCommand { get; }
   /// <summary>
   /// Command used to approve a rental request.
   /// </summary>
   public ICommand ApproveRentalCommand { get; }
   /// <summary>
   /// Command used to reject a rental request.
   /// </summary>
   public ICommand RejectRentalCommand { get; }
   /// <summary>
   /// Command used to mark a rental as returned.
   /// </summary>
   public ICommand MarkReturnedCommand { get; }
   /// <summary>
   /// Command used to mark a rental as completed.
   /// </summary>
   public ICommand MarkCompletedCommand { get; }
   /// <summary>
   /// Command used to open the review page for a rental.
   /// This is assigned from the RentalsPage because it requires navigation.
   /// </summary>
   public ICommand ReviewRentalCommand { get; set; }
   public RentalsViewModel(IApiService apiService)
   {
       _apiService = apiService;
       // Bind UI commands to rental actions
       LoadRentalsCommand = new Command(async () => await LoadRentalsAsync());
       ApproveRentalCommand = new Command<Rental>(async rental => await UpdateStatusAsync(rental, "Approved"));
       RejectRentalCommand = new Command<Rental>(async rental => await UpdateStatusAsync(rental, "Rejected"));
       MarkReturnedCommand = new Command<Rental>(async rental => await UpdateStatusAsync(rental, "Returned"));
       MarkCompletedCommand = new Command<Rental>(async rental => await UpdateStatusAsync(rental, "Completed"));
       // Load rentals when the ViewModel is created
       _ = LoadRentalsAsync();
   }
   /// <summary>
   /// Loads outgoing rentals for the current user from the API.
   /// </summary>
   private async Task LoadRentalsAsync()
   {
       try
       {
           // Clear existing rentals before reloading
           Rentals.Clear();
           // Retrieve rentals from API
           var rentals = await _apiService.GetOutgoingRentalsAsync();
           // Populate observable collection
           foreach (var rental in rentals)
           {
               Rentals.Add(rental);
           }
       }
       catch (Exception ex)
       {
           // Display error if rental loading fails
           await Application.Current.MainPage.DisplayAlert(
               "Error",
               ex.Message,
               "OK");
       }
   }
   /// <summary>
   /// Updates the selected rental to a new status using the API.
   /// Refreshes the rental list after a successful update.
   /// </summary>
   private async Task UpdateStatusAsync(Rental? rental, string status)
   {
       if (rental is null)
       {
           return;
       }
       try
       {
           // Send status update to API
           await _apiService.UpdateRentalStatusAsync(rental.Id, status);
           // Notify user of successful status update
           await Application.Current.MainPage.DisplayAlert(
               "Success",
               $"Rental marked as {status}",
               "OK");
           // Refresh list so UI shows latest status
           await LoadRentalsAsync();
       }
       catch (Exception ex)
       {
           // Display API or validation error
           await Application.Current.MainPage.DisplayAlert(
               "Error",
               ex.Message,
               "OK");
       }
   }
}