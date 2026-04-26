using StarterApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using StarterApp.Views;

namespace StarterApp.Views;

public partial class MainPage : ContentPage
{
    private readonly ItemsListPage _itemsListPage;

    public MainPage(MainViewModel viewModel, ItemsListPage itemsListPage)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _itemsListPage = itemsListPage;
    }

    private async void OnViewItemsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_itemsListPage);
    }
}