using ShopEye.Models;
using ShopEye.Services.Repositories;
using ShopEye.Services.Database;

namespace ShopEye.Views;

[QueryProperty(nameof(Id), "Id")]
public partial class MoreInfoPage : ContentPage
{
    private readonly IDatabaseService _databaseService;
    private int _itemId;

    public MoreInfoPage(IDatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    public string Id
    {
        set
        {
            _itemId = int.Parse(value);
            Task.Run(async () => await LoadItemDetails(_itemId));
        }
    }

    private async Task LoadItemDetails(int id)
    {
        var item = await _databaseService.GetItemByIdAsync(id);
        if (item != null)
        {
            Dispatcher.Dispatch(() =>
            {
                itemNameTitle.Text = item.Title;
                itemNameLabel.Text = item.Title;
                itemManufacturer.Text = item.Manufacturer;
                itemScanTime.Text = item.Scandate.ToString();
                itemUpc.Text = item.UPC;
            });
        }
    }

    private void btnBack_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}