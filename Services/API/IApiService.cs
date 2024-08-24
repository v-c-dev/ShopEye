using ShopEye.Models.Entities;

namespace ShopEye.Services.API
{
    public interface IApiService
    {
        Task<ItemEntity> GetItemDetailsAsync(string barcode);
    }
}