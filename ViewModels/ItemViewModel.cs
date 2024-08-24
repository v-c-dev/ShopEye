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

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _manufacturer;
        public string Manufacturer
        {
            get => _manufacturer;
            set => SetProperty(ref _manufacturer, value);
        }

        //private string _origin;
        //public string Origin
        //{
        //    get => _origin;
        //    set => SetProperty(ref _origin, value);
        //}

        private string _upc;
        public string UPC
        {
            get => _upc;
            set => SetProperty(ref _upc, value);
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
                Name = Name,
                Manufacturer = Manufacturer,
                //Origin = Origin,
                Scandate = DateTime.Now,
                UPC = UPC
            };

            await _databaseService.AddItemAsync(newItem);
            Items.Add(newItem);

            // Clear input fields after adding the item
            Name = string.Empty;
            Manufacturer = string.Empty;
            //Origin = string.Empty;
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