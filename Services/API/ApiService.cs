using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using ShopEye.Models;

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

        public async Task<Item> GetItemDetailsAsync(string barcode)
        {
            var url = $"https://api.barcodespider.com/v1/lookup?token={_apiKey}&barcode={barcode}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Item>(content);
        }
    }
}