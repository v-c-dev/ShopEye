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
        try
        {
            var first = e.Results?.FirstOrDefault();
            if (first == null)
            {
                return;
            }

            var apiService = App.Current.Handler.MauiContext.Services.GetService<IApiService>();
            var databaseService = App.Current.Handler.MauiContext.Services.GetService<IDatabaseService>();

            var item = await apiService.GetItemDetailsAsync(first.Value);
            await databaseService.AddItemAsync(item);

            // Navigate to MoreInfoPage with the scanned item details
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(MoreInfoPage)}?Id={item.Id}");
            });
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private void BtnBack_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}