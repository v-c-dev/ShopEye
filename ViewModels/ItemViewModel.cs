using System.Collections.ObjectModel;
using System.Windows.Input;
using ShopEye.Models.Entities;
using ShopEye.Services.Database;


namespace ShopEye.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private readonly IDatabaseService _databaseService;

        public ObservableCollection<ItemEntity> Items { get; private set; }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _manufacturer;
        public string Manufacturer
        {
            get => _manufacturer;
            set => SetProperty(ref _manufacturer, value);
        }

        private string _upc;
        public string UPC
        {
            get => _upc;
            set => SetProperty(ref _upc, value);
        }

        private string _brand;
        public string Brand
        {
            get => _brand;
            set => SetProperty(ref _brand, value);
        }

        private string _category;
        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private string _model;
        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        private DateTime _scandate;
        public DateTime ScanDate
        {
            get => _scandate;
            set => SetProperty(ref _scandate, value);
        }

        public ICommand AddItemCommand { get; }
        public ICommand LoadItemsCommand { get; }

        public ItemViewModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            Items = new ObservableCollection<ItemEntity>();

            AddItemCommand = new Command(async () => await AddItem());
            LoadItemsCommand = new Command(async () => await LoadItems());

            // Load items when the ViewModel is initialized
            Task.Run(async () => await LoadItems());
        }

        private async Task AddItem()
        {
            var newItem = new ItemEntity
            {
                Title = Title,
                Manufacturer = Manufacturer,
                Brand = Brand,
                Category = Category,
                Model = Model,
                UPC = UPC,
                Scandate = DateTime.Now
            };

            await _databaseService.AddItemAsync(newItem);
            Items.Add(newItem);

            // Clear input fields after adding the item
            Title = string.Empty;
            Manufacturer = string.Empty;
            Brand = string.Empty;
            Category = string.Empty;
            Model = string.Empty;
            UPC = string.Empty;
        }

        private async Task LoadItems()
        {
            var items = await _databaseService.GetItemsAsync();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}