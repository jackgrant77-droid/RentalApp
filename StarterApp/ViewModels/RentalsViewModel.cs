using System.Collections.ObjectModel;
using System.Windows.Input;
using StarterApp.Database.Models;
using StarterApp.Services;
namespace StarterApp.ViewModels;
public class RentalsViewModel
{
   private readonly IApiService _apiService;
   public ObservableCollection<Rental> Rentals { get; } = new();
   public ICommand LoadRentalsCommand { get; }
   public RentalsViewModel(IApiService apiService)
   {
       _apiService = apiService;
       LoadRentalsCommand = new Command(async () => await LoadRentalsAsync());
   }
   private async Task LoadRentalsAsync()
   {
       try
       {
           Rentals.Clear();
           var rentals = await _apiService.GetOutgoingRentalsAsync();
           await Application.Current.MainPage.DisplayAlert(
               "Debug",
               $"Loaded {rentals.Count} rentals",
               "OK");
           foreach (var rental in rentals)
           {
               Rentals.Add(rental);
           }
       }
       catch (Exception ex)
       {
           await Application.Current.MainPage.DisplayAlert(
               "Error",
               ex.ToString(),
               "OK");
       }
   }
}