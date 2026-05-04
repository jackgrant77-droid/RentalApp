using StarterApp.ViewModels;
namespace StarterApp.Views;
/// <summary>
/// Page used to create and submit a review for a rental.
/// Binds to CreateReviewViewModel to handle user input and actions.
/// </summary>
public partial class CreateReviewPage : ContentPage
{
   /// <summary>
   /// Initializes the page and sets the ViewModel as the binding context.
   /// </summary>
   public CreateReviewPage(CreateReviewViewModel viewModel)
   {
       InitializeComponent();
       // Connects the UI to the ViewModel (MVVM pattern)
       BindingContext = viewModel;
   }
}