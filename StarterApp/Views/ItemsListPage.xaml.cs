using StarterApp.ViewModels;

namespace StarterApp.Views;

public partial class ItemsListPage : ContentPage
{
    private readonly ItemsListViewModel _viewModel;
    private readonly CreateItemPage _createItemPage;

    public ItemsListPage(ItemsListViewModel viewModel, CreateItemPage createItemPage)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _createItemPage = createItemPage;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadItemsCommand.Execute(null);
    }

    private async void OnCreateItemClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_createItemPage);
    }
}