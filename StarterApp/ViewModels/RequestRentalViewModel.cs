using System.Windows.Input;
using StarterApp.Services;

namespace StarterApp.ViewModels;

public class RequestRentalViewModel
{
    private readonly IApiService _apiService;

    public int ItemId { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Today.AddDays(1);
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(2);

    public ICommand RequestRentalCommand { get; }

    public RequestRentalViewModel(IApiService apiService)
    {
        _apiService = apiService;
        RequestRentalCommand = new Command(async () => await RequestRentalAsync());
    }

    private async Task RequestRentalAsync()
    {
        try
        {
            await _apiService.RequestRentalAsync(ItemId, StartDate, EndDate);

            await Application.Current.MainPage.DisplayAlert(
                "Success",
                "Rental request submitted!",
                "OK");
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