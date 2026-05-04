using StarterApp.ViewModels;

namespace StarterApp.Views;

/// <summary>

/// Page used to create a new item.

/// Binds to CreateItemViewModel to handle user input and actions.

/// </summary>

public partial class CreateItemPage : ContentPage

{

    /// <summary>

    /// Initializes the page and sets the ViewModel as the binding context.

    /// </summary>

    public CreateItemPage(CreateItemViewModel viewModel)

    {

        InitializeComponent();

        // Connects the UI to the ViewModel (MVVM pattern)

        BindingContext = viewModel;

    }

}
 