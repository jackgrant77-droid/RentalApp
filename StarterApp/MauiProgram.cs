using Microsoft.Extensions.Logging;
using StarterApp.ViewModels;
using StarterApp.Database.Data;
using StarterApp.Views;
using StarterApp.Services;
using StarterApp.Database.Data.Repositories;
namespace StarterApp;
/// <summary>
/// Configures and builds the .NET MAUI application.
/// This includes fonts, database context, services, repositories,
/// ViewModels, and pages used throughout the app.
/// </summary>
public static class MauiProgram
{
   /// <summary>
   /// Creates and configures the MAUI application instance.
   /// </summary>
   public static MauiApp CreateMauiApp()
   {
       var builder = MauiApp.CreateBuilder();
       builder
           .UseMauiApp<App>()
           .ConfigureFonts(fonts =>
           {
               fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
               fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
           });
       // Register database context
       builder.Services.AddDbContext<AppDbContext>();
       // Register core application services
       builder.Services.AddSingleton<IAuthenticationService, ApiAuthenticationService>();
       builder.Services.AddSingleton<INavigationService, NavigationService>();
       builder.Services.AddSingleton<IApiService, ApiService>();
       builder.Services.AddSingleton<ILocationService, LocationService>();
       // Register shell and app-level classes
       builder.Services.AddSingleton<AppShellViewModel>();
       builder.Services.AddSingleton<AppShell>();
       builder.Services.AddSingleton<App>();
       // Register main pages and ViewModels
       builder.Services.AddTransient<MainViewModel>();
       builder.Services.AddTransient<MainPage>();
       builder.Services.AddSingleton<LoginViewModel>();
       builder.Services.AddTransient<LoginPage>();
       builder.Services.AddSingleton<RegisterViewModel>();
       builder.Services.AddTransient<RegisterPage>();
       // Register user management pages and ViewModels
       builder.Services.AddTransient<UserListViewModel>();
       builder.Services.AddTransient<UserListPage>();
       builder.Services.AddTransient<UserDetailViewModel>();
       builder.Services.AddTransient<UserDetailPage>();
       // Register temporary/test pages
       builder.Services.AddSingleton<TempViewModel>();
       builder.Services.AddTransient<TempPage>();
       // Register item repository, ViewModels, and pages
       builder.Services.AddScoped<IItemRepository, ItemRepository>();
       builder.Services.AddTransient<ItemsListViewModel>();
       builder.Services.AddTransient<ItemsListPage>();
       builder.Services.AddTransient<CreateItemViewModel>();
       builder.Services.AddTransient<CreateItemPage>();
       // Register rental repository, services, ViewModels, and pages
       builder.Services.AddScoped<IRentalRepository, RentalRepository>();
       builder.Services.AddScoped<IRentalService, RentalService>();
       builder.Services.AddTransient<RequestRentalViewModel>();
       builder.Services.AddTransient<RequestRentalPage>();
       builder.Services.AddTransient<RentalsViewModel>();
       builder.Services.AddTransient<RentalsPage>();
       // Register nearby item search pages and ViewModels
       builder.Services.AddTransient<NearbyItemsViewModel>();
       builder.Services.AddTransient<NearbyItemsPage>();
       // Register review pages and ViewModels
       builder.Services.AddTransient<CreateReviewViewModel>();
       builder.Services.AddTransient<CreateReviewPage>();
#if DEBUG
       // Enable debug logging during development
       builder.Logging.AddDebug();
#endif
       return builder.Build();
   }
}