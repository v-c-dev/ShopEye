using System.Net.NetworkInformation;
using ShopEye.Models.Entities;
using ShopEye.Services.API;
using ShopEye.Services.Database;
using ZXing.Net.Maui;


namespace ShopEye.Views;

public partial class ScanPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly IDatabaseService _databaseService;

    public ScanPage(IApiService apiService, IDatabaseService databaseService)
    {
        InitializeComponent();
        _apiService = apiService;
        _databaseService = databaseService;

        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            Multiple = true,
            AutoRotate = true,
            TryHarder = true
        };
        barcodeReader.CameraLocation = CameraLocation.Rear;
    }

    private async void barcodeReader_OnBarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
    {
        var first = e.Results?.FirstOrDefault();

        if (first == null)
        {
            return;
        }

        Dispatcher.DispatchAsync(async () =>
        {
            if (first.Format == BarcodeFormat.QrCode)
            {
                await DisplayAlert("QR Code detected", "QR codes are not supported", "OK");
                return;
            }

            string scannedUPC = first.Value;

            // Fetch item details from the API
            var itemDetails = await _apiService.GetItemDetailsAsync(scannedUPC);

            if (itemDetails == null)
            {
                await DisplayAlert("Error", "No item found for the scanned UPC", "OK");
                return;
            }

            // Save item details to the SQLite database
            await _databaseService.AddItemAsync(itemDetails);

            // Navigate to MoreInfoPage with the new item's ID
            await Shell.Current.GoToAsync($"{nameof(MoreInfoPage)}?Id={itemDetails.Id}");
        });
    }

    private void BtnBack_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}