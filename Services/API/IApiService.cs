using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEye.Models;

namespace ShopEye.Services.API
{
    interface IApiService
    {
        Task<Item> GetItemDetailsAsync(string barcode);
    }
}