using System.Windows.Input;
using StarterApp.Services;

namespace StarterApp.ViewModels;

public class RequestRentalViewModel
{
    private readonly IRentalService _rentalService;

    public int ItemId { get; set; }
    public decimal DailyRate { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

    public ICommand RequestRentalCommand { get; }

    public RequestRentalViewModel(IRentalService rentalService)
    {
        _rentalService = rentalService;
        RequestRentalCommand = new Command(async () => await RequestRentalAsync());
    }

    private async Task RequestRentalAsync()
    {
        await _rentalService.RequestRentalAsync(
            ItemId,
            "test-user",
            StartDate,
            EndDate,
            DailyRate);

        await Application.Current.MainPage.DisplayAlert(
            "Success",
            "Rental request submitted!",
            "OK");
    }
}