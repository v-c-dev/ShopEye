using ShopEye.Models.Entities;

namespace ShopEye.Services.Database
{
    public interface IDatabaseService
    {
        Task InitializeAsync();
        Task AddItemAsync(ItemEntity item);
        Task<List<ItemEntity>> GetItemsAsync();
        Task<ItemEntity> GetItemByIdAsync(int id);
        Task DeleteItemAsync(int id);
    }
}