using StarterApp.ViewModels;

namespace StarterApp.Views;

/// <summary>

/// Page responsible for user login.

/// Binds to LoginViewModel to handle authentication logic.

/// </summary>

public partial class LoginPage : ContentPage

{

    /// <summary>

    /// Initializes the page and sets the ViewModel as the binding context.

    /// </summary>

    public LoginPage(LoginViewModel viewModel)

    {

        InitializeComponent();

        // Connects the UI to the ViewModel (MVVM pattern)

        BindingContext = viewModel;

    }

    /// <summary>

    /// Called when the page appears.

    /// Sets focus to the email field and pre-fills login details for testing.

    /// </summary>

    protected override void OnAppearing()

    {

        base.OnAppearing();

        // Set focus to email input field

        EmailEntry.Focus();

        // Pre-fill credentials for testing/demo purposes

        EmailEntry.Text = "admin@company.com";

        PasswordEntry.Text = "Admin123!";

    }

}
 