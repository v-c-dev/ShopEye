using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using ShopEye.Models;
using ShopEye.Models.Entities;

namespace ShopEye.Services.API
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ApiService(string apiKey)
        {
            _httpClient = new HttpClient();
            _apiKey = apiKey;
        }

        public async Task<ItemEntity> GetItemDetailsAsync(string barcode)
        {
            var url = $"https://api.barcodespider.com/v1/lookup?token={_apiKey}&upc={barcode}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var itemDetails = JsonSerializer.Deserialize<BarcodeSpiderResponse>(content);

            if (itemDetails?.ItemAttributes == null)
            {
                return null;
            }

            return new ItemEntity
            {
                Name = itemDetails.ItemAttributes.Title,
                Manufacturer = itemDetails.ItemAttributes.Brand,
                UPC = barcode
            };
        }
    }

    public class BarcodeSpiderResponse
    {
        public ItemAttributes ItemAttributes { get; set; }
    }

    public class ItemAttributes
    {
        public string Title { get; set; }
        public string Brand { get; set; }
    }
}