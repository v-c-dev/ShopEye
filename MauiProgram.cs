using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopEye.Services.API;
using ShopEye.Services.Database;
using ShopEye.Views;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using Google.Cloud.SecretManager.V1;
using Google.Apis.Auth.OAuth2;
using Grpc.Core;
using Grpc.Auth;
using System.Threading.Channels;
using Grpc.Net.Client;

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

            #region barcodespider
            // Fetch the API key from Google Cloud Secret Manager
            string apiKey = GetApiKeyFromSecretManager().Result;
            builder.Services.AddSingleton<IApiService>(s => new ApiService(apiKey));
            #endregion

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<ScanPage>();
            builder.Services.AddTransient<MoreInfoPage>();

            return builder.Build();
        }

        public static async Task<string> GetApiKeyFromSecretManager()
        {
            // Authenticate using the service account
            GoogleCredential credential = await GoogleCredential.GetApplicationDefaultAsync();

            // Create CallCredentials from the Google Credential
            CallCredentials callCredentials = credential.ToCallCredentials();

            // Combine the CallCredentials with SslCredentials
            ChannelCredentials channelCredentials = ChannelCredentials.Create(new SslCredentials(), callCredentials);

            // Define the gRPC channel address (Secret Manager)
            var grpcChannel = GrpcChannel.ForAddress("https://secretmanager.googleapis.com", new GrpcChannelOptions
            {
                Credentials = channelCredentials
            });

            // Create the Secret Manager client using the gRPC channel
            var client = new SecretManagerServiceClientBuilder
            {
                ChannelCredentials = channelCredentials
            }.Build();

            SecretVersionName secretVersionName = new SecretVersionName("GoogleProjectId", "GoogleSecretId", "latest");

            // Access the secret
            AccessSecretVersionResponse result = await client.AccessSecretVersionAsync(secretVersionName);
            string apiKey = result.Payload.Data.ToStringUtf8();

            return apiKey;
        }

    }
}
