using StarterApp.ViewModels;
namespace StarterApp.Views;
/// <summary>
/// Page that displays items near the user's current location.
/// Binds to NearbyItemsViewModel to handle location and data retrieval.
/// </summary>
public partial class NearbyItemsPage : ContentPage
{
   /// <summary>
   /// Initializes the page and sets the ViewModel as the binding context.
   /// </summary>
   public NearbyItemsPage(NearbyItemsViewModel viewModel)
   {
       InitializeComponent();
       // Connects the UI to the ViewModel (MVVM pattern)
       BindingContext = viewModel;
   }
}