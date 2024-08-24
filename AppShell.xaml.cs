using ShopEye.Views;

namespace ShopEye
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ScanPage), typeof(ScanPage));
            Routing.RegisterRoute(nameof(ScanHistoryPage), typeof(ScanHistoryPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(MoreInfoPage), typeof(MoreInfoPage));
        }
    }
}