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
            try
            {
                var url = $"https://api.barcodespider.com/v1/lookup?token={_apiKey}&upc={barcode}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(content);
                var itemAttributes = jsonDocument.RootElement.GetProperty("item_attributes");

                return new ItemEntity
                {
                    Title = itemAttributes.TryGetProperty("title", out var titleProp) ? titleProp.GetString() : string.Empty,
                    UPC = itemAttributes.TryGetProperty("upc", out var upcProp) ? upcProp.GetString() : string.Empty,
                    EAN = itemAttributes.TryGetProperty("ean", out var eanProp) ? eanProp.GetString() : string.Empty,
                    ParentCategory = itemAttributes.TryGetProperty("parent_category", out var parentCategoryProp) ? parentCategoryProp.GetString() : string.Empty,
                    Category = itemAttributes.TryGetProperty("category", out var categoryProp) ? categoryProp.GetString() : string.Empty,
                    Brand = itemAttributes.TryGetProperty("brand", out var brandProp) ? brandProp.GetString() : string.Empty,
                    Model = itemAttributes.TryGetProperty("model", out var modelProp) ? modelProp.GetString() : string.Empty,
                    Manufacturer = itemAttributes.TryGetProperty("manufacturer", out var manufacturerProp) ? manufacturerProp.GetString() : string.Empty,
                    Publisher = itemAttributes.TryGetProperty("publisher", out var publisherProp) ? publisherProp.GetString() : string.Empty,
                    ASIN = itemAttributes.TryGetProperty("asin", out var asinProp) ? asinProp.GetString() : string.Empty,
                    Color = itemAttributes.TryGetProperty("color", out var colorProp) ? colorProp.GetString() : string.Empty,
                    ImageUrl = itemAttributes.TryGetProperty("image", out var imageProp) ? imageProp.GetString() : string.Empty,
                    IsAdult = itemAttributes.TryGetProperty("is_adult", out var isAdultProp) && isAdultProp.GetString() == "1",
                    Description = itemAttributes.TryGetProperty("description", out var descriptionProp) ? descriptionProp.GetString() : string.Empty,
                    LowestPrice = itemAttributes.TryGetProperty("lowest_price", out var lowestPriceProp) && decimal.TryParse(lowestPriceProp.GetString(), out var lowestPrice) ? lowestPrice : 0m,
                    HighestPrice = itemAttributes.TryGetProperty("highest_price", out var highestPriceProp) && decimal.TryParse(highestPriceProp.GetString(), out var highestPrice) ? highestPrice : 0m
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}