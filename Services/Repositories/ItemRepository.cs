using ShopEye.Models;

namespace ShopEye.Services.Repositories
{
    public static class ItemRepository
    {
        public static List<Item> _items = new List<Item>()
        {
            new Item { Id = 1, Title="Test1", Manufacturer="t1 manufacturer", UPC = "098595739827"},
            new Item { Id = 2, Title="Test2", Manufacturer="t2 manufacturer", UPC = "123456789"},
            new Item { Id = 3, Title="Test3", Manufacturer="t3 manufacturer", UPC = "0987654321"},
            new Item { Id = 4, Title="Test4", Manufacturer="t4 manufacturer", UPC = "983746183836"},
        };

        public static List<Item> GetAllItems() => _items;

        public static Item GetItemById(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public static Item DeleteById(int id)
        {
            return null;
        }
    }
}