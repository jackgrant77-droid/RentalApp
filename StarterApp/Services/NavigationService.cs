namespace StarterApp.Services;
/// <summary>
/// Provides navigation functionality using .NET MAUI Shell.
/// This service abstracts navigation logic away from ViewModels,
/// supporting the MVVM pattern.
/// </summary>
public class NavigationService : INavigationService
{
   /// <summary>
   /// Navigates to a specified route.
   /// </summary>
   public async Task NavigateToAsync(string route)
   {
       await Shell.Current.GoToAsync(route);
   }
   /// <summary>
   /// Navigates to a specified route with parameters.
   /// </summary>
   public async Task NavigateToAsync(string route, Dictionary<string, object> parameters)
   {
       await Shell.Current.GoToAsync(route, parameters);
   }
   /// <summary>
   /// Navigates back to the previous page.
   /// </summary>
   public async Task NavigateBackAsync()
   {
       await Shell.Current.GoToAsync("..");
   }
   /// <summary>
   /// Navigates to the root page (e.g., login page).
   /// </summary>
   public async Task NavigateToRootAsync()
   {
       await Shell.Current.GoToAsync("//login");
   }
   /// <summary>
   /// Clears the navigation stack and returns to the root page.
   /// </summary>
   public async Task PopToRootAsync()
   {
       await Shell.Current.Navigation.PopToRootAsync();
   }
}