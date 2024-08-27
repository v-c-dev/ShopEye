using ShopEye.Models;
using ShopEye.Models.Entities;
using ShopEye.Services.Database;
namespace ShopEye.Views;

public partial class ScanHistoryPage : ContentPage
{
    private readonly IDatabaseService _databaseService;

    public ScanHistoryPage()
    {
        InitializeComponent();

        // Fetch the database service from the DI container
        _databaseService = App.Current.Handler.MauiContext.Services.GetService<IDatabaseService>();

        // Load items from the database
        LoadItemsAsync();
    }

    private async Task LoadItemsAsync()
    {
        // Fetch all items from the SQLite database
        var items = await _databaseService.GetItemsAsync();

        // Ensure that each product is listed only once
        var distinctItems = items.GroupBy(i => i.Id).Select(g => g.First()).ToList();

        // Bind the distinct items to the ListView
        scanHistoryList.ItemsSource = distinctItems;
    }

    private void btnBack_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private async void scanHistoryList_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        // avoid the item selection to be called twice
        if (scanHistoryList.SelectedItem != null)
        {
            // Go to More Info Page
            var selectedItem = (ItemEntity)scanHistoryList.SelectedItem;
            await Shell.Current.GoToAsync($"{nameof(MoreInfoPage)}?Id={selectedItem.Id}");

            // Deselect item
            scanHistoryList.SelectedItem = null;
        }
    }

    private void scanHistoryList_OnItemTapped(object? sender, ItemTappedEventArgs e)
    {
        // Deselect item
        scanHistoryList.SelectedItem = null;
    }
}