using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEye.Models.Entities;

namespace ShopEye.Services.Database
{
    public interface IDatabaseService
    {
        Task InitializeAsync();
        Task AddItemAsync(ItemEntity item);
        Task<List<ItemEntity>> GetItemsAsync();
        Task<ItemEntity> GetItemByIdAsync(int id);
        Task UpdateItemAsync(ItemEntity item);
        Task DeleteItemAsync(int id);
    }
}