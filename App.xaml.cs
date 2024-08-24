using ShopEye.Services.Database;

namespace ShopEye
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // initialize local sqlite database
            DatabaseService.Initialize();

            MainPage = new AppShell();
        }
    }
}