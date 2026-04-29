using System.Windows.Input;
using StarterApp.Services;

namespace StarterApp.ViewModels;

public class CreateReviewViewModel
{
    private readonly IApiService _apiService;

    public int RentalId { get; set; }

    public int Rating { get; set; } = 5;

    public string Comment { get; set; } = string.Empty;

    public ICommand SubmitReviewCommand { get; }

    public CreateReviewViewModel(IApiService apiService)
    {
        _apiService = apiService;
        SubmitReviewCommand = new Command(async () => await SubmitReviewAsync());
    }

    private async Task SubmitReviewAsync()
    {
        try
        {
            await _apiService.SubmitReviewAsync(RentalId, Rating, Comment);

            await Application.Current.MainPage.DisplayAlert(
                "Success",
                "Review submitted!",
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