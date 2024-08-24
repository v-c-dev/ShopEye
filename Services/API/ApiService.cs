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
            var url = $"https://api.barcodespider.com/v1/lookup?token={_apiKey}&barcode={barcode}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(content);
            var itemAttributes = jsonDocument.RootElement.GetProperty("item_attributes");

            return new ItemEntity
            {
                Title = itemAttributes.GetProperty("title").GetString(),
                UPC = itemAttributes.GetProperty("upc").GetString(),
                EAN = itemAttributes.GetProperty("ean").GetString(),
                ParentCategory = itemAttributes.GetProperty("parent_category").GetString(),
                Category = itemAttributes.GetProperty("category").GetString(),
                Brand = itemAttributes.GetProperty("brand").GetString(),
                Model = itemAttributes.GetProperty("model").GetString(),
                MPN = itemAttributes.GetProperty("mpn").GetString(),
                Manufacturer = itemAttributes.GetProperty("manufacturer").GetString(),
                Publisher = itemAttributes.GetProperty("publisher").GetString(),
                ASIN = itemAttributes.GetProperty("asin").GetString(),
                Color = itemAttributes.GetProperty("color").GetString(),
                Size = itemAttributes.GetProperty("size").GetString(),
                Weight = itemAttributes.GetProperty("weight").GetString(),
                ImageUrl = itemAttributes.GetProperty("image").GetString(),
                IsAdult = itemAttributes.GetProperty("is_adult").GetString() == "1",
                Description = itemAttributes.GetProperty("description").GetString(),
                LowestPrice = decimal.Parse(itemAttributes.GetProperty("lowest_price").GetString()),
                HighestPrice = decimal.Parse(itemAttributes.GetProperty("highest_price").GetString())
            };
        }
    }
}