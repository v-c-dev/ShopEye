using ShopEye.Models;
using ShopEye.Services.Repositories;

namespace ShopEye.Views;

[QueryProperty(nameof(Id), "Id")]
public partial class MoreInfoPage : ContentPage
{
    private Item item;
    public MoreInfoPage()
    {
        InitializeComponent();
    }

    public string Id
    {
        set
        {
            item = ItemRepository.GetItemById(int.Parse(value));
            if (item != null)
            {
                itemNameTitle.Text = item.Name;
                itemNameLabel.Text = item.Name;
                itemManufacturer.Text = item.Manufacturer;
                //itemCountry.Text = item.Origin;
                itemScanTime.Text = item.Scandate.ToString();
                itemUpc.Text = item.UPC;
            }
        }
    }

    private void btnBack_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}