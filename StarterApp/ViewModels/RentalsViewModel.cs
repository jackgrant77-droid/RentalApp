using System.Collections.ObjectModel;
using System.Windows.Input;
using StarterApp.Database.Data.Repositories;
using StarterApp.Database.Models;

namespace StarterApp.ViewModels;

public class RentalsViewModel
{
    private readonly IRentalRepository _rentalRepository;

    public ObservableCollection<Rental> Rentals { get; } = new();

    public ICommand LoadRentalsCommand { get; }

    public RentalsViewModel(IRentalRepository rentalRepository)
    {
        _rentalRepository = rentalRepository;
        LoadRentalsCommand = new Command(async () => await LoadRentalsAsync());
    }

    private async Task LoadRentalsAsync()
    {
        Rentals.Clear();

        var rentals = await _rentalRepository.GetAllAsync();

        foreach (var rental in rentals)
        {
            Rentals.Add(rental);
        }
    }
}