using ShopEye.Views;

namespace ShopEye
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnScan_OnClicked(object? sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(ScanPage));
        }

        private void BtnHistory_OnClicked(object? sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(ScanHistoryPage));
        }

        private void BtnAbout_OnClicked(object? sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(AboutPage));
        }
    }

}