using System.Windows.Input;
using StarterApp.Services;
namespace StarterApp.ViewModels;
/// <summary>
/// ViewModel responsible for creating and submitting reviews for rentals.
/// Handles user input and communicates with the API service.
/// </summary>
public class CreateReviewViewModel
{
   private readonly IApiService _apiService;
   /// <summary>
   /// The ID of the rental being reviewed.
   /// </summary>
   public int RentalId { get; set; }
   /// <summary>
   /// The rating given by the user (default is 5).
   /// </summary>
   public int Rating { get; set; } = 5;
   /// <summary>
   /// The review comment entered by the user.
   /// </summary>
   public string Comment { get; set; } = string.Empty;
   /// <summary>
   /// Command triggered when the user submits a review.
   /// </summary>
   public ICommand SubmitReviewCommand { get; }
   public CreateReviewViewModel(IApiService apiService)
   {
       _apiService = apiService;
       // Bind submit button to SubmitReviewAsync method
       SubmitReviewCommand = new Command(async () => await SubmitReviewAsync());
   }
   /// <summary>
   /// Sends the review data to the API and displays feedback to the user.
   /// </summary>
   private async Task SubmitReviewAsync()
   {
       try
       {
           // Submit review to API
           await _apiService.SubmitReviewAsync(RentalId, Rating, Comment);
           // Notify user of success
           await Application.Current.MainPage.DisplayAlert(
               "Success",
               "Review submitted!",
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