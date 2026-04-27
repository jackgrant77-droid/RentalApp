using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class ItemsListPage : ContentPage
{
    private readonly CreateItemPage _createItemPage;

    public ItemsListPage(ItemsListViewModel viewModel, CreateItemPage createItemPage)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _createItemPage = createItemPage;
    }

    private async void OnCreateItemClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_createItemPage);
    }
}