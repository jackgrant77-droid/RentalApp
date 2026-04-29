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
    public ICommand ApproveRentalCommand { get; }
    public ICommand RejectRentalCommand { get; }

    public RentalsViewModel(IApiService apiService)
    {
        _apiService = apiService;

        LoadRentalsCommand = new Command(async () => await LoadRentalsAsync());
        ApproveRentalCommand = new Command<Rental>(async rental => await UpdateStatusAsync(rental, "Approved"));
        RejectRentalCommand = new Command<Rental>(async rental => await UpdateStatusAsync(rental, "Rejected"));

        _ = LoadRentalsAsync();
    }

    private async Task LoadRentalsAsync()
    {
        try
        {
            Rentals.Clear();

            var rentals = await _apiService.GetOutgoingRentalsAsync();

            foreach (var rental in rentals)
            {
                Rentals.Add(rental);
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
    }

    private async Task UpdateStatusAsync(Rental? rental, string status)
    {
        if (rental is null)
        {
            return;
        }

        try
        {
            await _apiService.UpdateRentalStatusAsync(rental.Id, status);

            await Application.Current.MainPage.DisplayAlert(
                "Success",
                $"Rental marked as {status}",
                "OK");

            await LoadRentalsAsync();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
    }
}