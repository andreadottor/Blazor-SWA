namespace Dottor.Blazor.SWA.Services
{
    using Dottor.Blazor.SWA.Models;
    using System.Net.Http.Json;
    using System.Text.Json;

    public class ShoppingService : IShoppingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ShoppingService> _logger;

        public ShoppingService(HttpClient httpClient, ILogger<ShoppingService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<ShoppingList>> GetShoppingListsAsync()
        {
            var results = await _httpClient.GetFromJsonAsync<DABResult<IEnumerable<ShoppingList>>>("data-api/rest/lists");

            if(results is null || results.Value is null)
            {
                _logger.LogWarning("No shopping lists found for the user.");
                return Enumerable.Empty<ShoppingList>();
            }

            return results.Value;
        }

        public async Task InsertShoppingList(ShoppingList shoppingList)
        {
            var response = await _httpClient.PostAsJsonAsync("data-api/rest/lists", shoppingList);
            response.EnsureSuccessStatusCode();
        }
    }
}
