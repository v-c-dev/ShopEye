using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopEye.Services.API;
using ShopEye.Services.Database;
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

            //TODO: Use Secure storage instead of appsettings
            #region barcodelookup
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            string apiKey = builder.Configuration["ApiSettings:BarcodeSpiderApiKey"];
            builder.Services.AddSingleton<IApiService>(s => new ApiService(apiKey));
            #endregion

#if DEBUG
            builder.Logging.AddDebug();
#endif



            return builder.Build();
        }
    }
}