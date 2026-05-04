using StarterApp.ViewModels;

namespace StarterApp.Views;

/// <summary>

/// Page used for user registration.

/// Binds to RegisterViewModel to handle user input and registration logic.

/// </summary>

public partial class RegisterPage : ContentPage

{

    /// <summary>

    /// Initializes the page and sets the ViewModel as the binding context.

    /// </summary>

    public RegisterPage(RegisterViewModel viewModel)

    {

        InitializeComponent();

        // Connects the UI to the ViewModel (MVVM pattern)

        BindingContext = viewModel;

    }

}
 