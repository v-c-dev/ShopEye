using ShopEye.Models;
using ShopEye.Services.Repositories;
namespace ShopEye.Views;

public partial class ScanHistoryPage : ContentPage
{
    public ScanHistoryPage()
    {
        InitializeComponent();
        List<Item> items = ItemRepository.GetAllItems();

        scanHistoryList.ItemsSource = items;
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
            //go to More Info Page
            await Shell.Current.GoToAsync($"{nameof(MoreInfoPage)}?Id={((Item)scanHistoryList.SelectedItem).Id}");
            //details Alert
            //DisplayAlert("Test", "test", "OK");
        }
    }

    private void scanHistoryList_OnItemTapped(object? sender, ItemTappedEventArgs e)
    {
        //deselect
        scanHistoryList.SelectedItem = null;
    }
}