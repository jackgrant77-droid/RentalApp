namespace StarterApp.Services;
/// <summary>
/// Defines navigation functionality for moving between pages within the application.
/// Abstracting navigation logic allows ViewModels to request navigation without
/// directly depending on UI frameworks.
/// </summary>
public interface INavigationService
{
   /// <summary>
   /// Navigates to a specified route.
   /// </summary>
   Task NavigateToAsync(string route);
   /// <summary>
   /// Navigates to a specified route with parameters.
   /// </summary>
   Task NavigateToAsync(string route, Dictionary<string, object> parameters);
   /// <summary>
   /// Navigates back to the previous page in the navigation stack.
   /// </summary>
   Task NavigateBackAsync();
   /// <summary>
   /// Navigates to the root page of the application.
   /// </summary>
   Task NavigateToRootAsync();
   /// <summary>
   /// Pops all pages and returns to the root page.
   /// </summary>
   Task PopToRootAsync();
}