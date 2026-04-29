using StarterApp.Database.Models;
using StarterApp.ViewModels;
namespace StarterApp.Views;
public partial class RentalsPage : ContentPage
{
   private readonly RentalsViewModel _viewModel;
   private readonly CreateReviewPage _createReviewPage;
   public RentalsPage(
       RentalsViewModel viewModel,
       CreateReviewPage createReviewPage)
   {
       InitializeComponent();
       _viewModel = viewModel;
       _createReviewPage = createReviewPage;
       _viewModel.ReviewRentalCommand = new Command<Rental>(
           async rental => await OpenReviewPageAsync(rental));
       BindingContext = _viewModel;
   }
   protected override void OnAppearing()
   {
       base.OnAppearing();
       _viewModel.LoadRentalsCommand.Execute(null);
   }
   private async Task OpenReviewPageAsync(Rental? rental)
   {
       if (rental is null)
       {
           return;
       }
       if (_createReviewPage.BindingContext is CreateReviewViewModel reviewViewModel)
       {
           reviewViewModel.RentalId = rental.Id;
       }
       await Navigation.PushAsync(_createReviewPage);
   }
}