using StarterApp.Database.Models;
using StarterApp.ViewModels;
namespace StarterApp.Views;
/// <summary>
/// Page that displays rentals and allows users to manage them,
/// including reviewing completed rentals.
/// Binds to RentalsViewModel.
/// </summary>
public partial class RentalsPage : ContentPage
{
   private readonly RentalsViewModel _viewModel;
   private readonly CreateReviewPage _createReviewPage;
   /// <summary>
   /// Initializes the page and injects required ViewModel and Review page.
   /// Sets up the Review command for navigation.
   /// </summary>
   public RentalsPage(
       RentalsViewModel viewModel,
       CreateReviewPage createReviewPage)
   {
       InitializeComponent();
       _viewModel = viewModel;
       _createReviewPage = createReviewPage;
       // Assign review command to open the review page
       _viewModel.ReviewRentalCommand = new Command<Rental>(
           async rental => await OpenReviewPageAsync(rental));
       // Bind ViewModel to UI
       BindingContext = _viewModel;
   }
   /// <summary>
   /// Called when the page appears.
   /// Loads the latest rental data.
   /// </summary>
   protected override void OnAppearing()
   {
       base.OnAppearing();
       _viewModel.LoadRentalsCommand.Execute(null);
   }
   /// <summary>
   /// Opens the review page for a selected rental.
   /// Passes the rental ID to the CreateReviewViewModel.
   /// </summary>
   private async Task OpenReviewPageAsync(Rental? rental)
   {
       if (rental is null)
       {
           return;
       }
       // Pass rental ID to review ViewModel
       if (_createReviewPage.BindingContext is CreateReviewViewModel reviewViewModel)
       {
           reviewViewModel.RentalId = rental.Id;
       }
       // Navigate to review page
       await Navigation.PushAsync(_createReviewPage);
   }
}