using System.Net.NetworkInformation;
using ZXing.Net.Maui;

namespace ShopEye.Views;

public partial class ScanPage : ContentPage
{
    public ScanPage()
    {
        InitializeComponent();
        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,

            //Limits to a given format --> Formats = ZXing.Net.Maui.BarcodeFormat.Ean13,
            Multiple = true,
            AutoRotate = true,
            TryHarder = true
        };
        barcodeReader.CameraLocation = CameraLocation.Rear;
    }

    private void barcodeReader_OnBarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
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
            }
            //UPC search logic
            //Add to Items
            await DisplayAlert("Barcode detected", first.Value, "OK");
        });
    }

    private void BtnBack_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}