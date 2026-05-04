using StarterApp.ViewModels;

namespace StarterApp.Views;

/// <summary>

/// Page used to request a rental for a selected item.

/// Binds to RequestRentalViewModel to handle user input and rental logic.

/// </summary>

public partial class RequestRentalPage : ContentPage

{

    /// <summary>

    /// Initializes the page and sets the ViewModel as the binding context.

    /// </summary>

    public RequestRentalPage(RequestRentalViewModel viewModel)

    {

        InitializeComponent();

        // Connects the UI to the ViewModel (MVVM pattern)

        BindingContext = viewModel;

    }

}
 