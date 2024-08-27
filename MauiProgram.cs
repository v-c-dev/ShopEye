using Microsoft.Extensions.Logging;
using ShopEye.Services.API;
using ShopEye.Services.Database;
using ShopEye.Views;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace ShopEye
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseBarcodeReader()


            #region ZXing-bug-prevention
                .ConfigureMauiHandlers(h =>
                {
                    h.AddHandler(typeof(ZXing.Net.Maui.Controls.CameraBarcodeReaderView), typeof(CameraBarcodeReaderViewHandler));
                    h.AddHandler(typeof(ZXing.Net.Maui.Controls.CameraView), typeof(CameraViewHandler));
                    h.AddHandler(typeof(ZXing.Net.Maui.Controls.BarcodeGeneratorView), typeof(BarcodeGeneratorViewHandler));
                });
            #endregion

            #region database
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mydatabase.db3");
            builder.Services.AddSingleton<IDatabaseService>(s => new DatabaseService(dbPath));
            #endregion

            // TODO: Google Secret Manager or Azure Key Vault would be ideal
            #region barcodespider
            builder.Services.AddSingleton<IApiService>(s => new ApiService("771e3a2a1093523a174d"));
            #endregion

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<ScanPage>();
            builder.Services.AddTransient<MoreInfoPage>();

            return builder.Build();
        }

    }
}
