namespace Dottor.Blazor.SWA.Services;

using Dottor.Blazor.SWA.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

public class ShoppingService : IShoppingService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ShoppingService> _logger;
    private readonly AuthenticationStateProvider _authStateProvider;

    public ShoppingService(HttpClient httpClient,
                           ILogger<ShoppingService> logger,
                           AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient        = httpClient;
        _logger            = logger;
        _authStateProvider = authenticationStateProvider;
    }

    #region Shopping lists

    public async Task<IEnumerable<ShoppingList>> GetShoppingListsAsync()
    {
        var orderBy = $"$orderby=favorite desc, title";
        var results = await _httpClient.GetFromJsonAsync<DABResult<IEnumerable<ShoppingList>>>($"data-api/rest/lists?{orderBy}");

        if (results is null || results.Value is null)
        {
            _logger.LogWarning("No shopping lists found for the user.");
            return Enumerable.Empty<ShoppingList>();
        }

        return results.Value;
    }

    public async Task<ShoppingList?> GetShoppingListAsync(Guid shoppingList)
    {
        var results = await _httpClient.GetFromJsonAsync<DABResult<IEnumerable<ShoppingList>>>($"data-api/rest/lists/id/{shoppingList}");

        if (results is null || results.Value is null)
        {
            _logger.LogWarning("No shopping lists with the specified id.");
            return null;
        }

        return results.Value.First();
    }

    public async Task InsertShoppingListAsync(ShoppingList shoppingList)
    {
        var response = await _httpClient.PostAsJsonAsync("data-api/rest/lists", shoppingList);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteShoppingListAsync(Guid shoppingList)
    {
        var response = await _httpClient.DeleteAsync($"data-api/rest/lists/id/{shoppingList}");
        response.EnsureSuccessStatusCode();
    }

    public async Task SetShoppingListFavoriteAsync(Guid shoppingList, bool favorite)
    {
        var authState   = await _authStateProvider.GetAuthenticationStateAsync();
        var userIdClaim = authState.User.FindFirst(ClaimTypes.NameIdentifier);

        var response    = await _httpClient.PatchAsJsonAsync($"data-api/rest/lists/id/{shoppingList}",
                new
                {
                    favorite = favorite,
                    userId = userIdClaim?.Value
                });
        response.EnsureSuccessStatusCode();
    }

    #endregion

    #region Shopping list items

    public async Task<IEnumerable<ShoppingItem>> GetItemsAsync(Guid shoppingList)
    {
        var filter  = $"$filter=shoppingListId eq {shoppingList}";
        var orderBy = $"$orderby=important desc, name";
        var results = await _httpClient.GetFromJsonAsync<DABResult<IEnumerable<ShoppingItem>>>($"data-api/rest/items?{filter}&{orderBy}");

        if (results is null || results.Value is null)
        {
            _logger.LogWarning("Shopping list is empty.");
            return Enumerable.Empty<ShoppingItem>();
        }

        return results.Value;
    }

    public async Task AddItemAsync(Guid shoppingList, ShoppingItem shoppingItem)
    {
        shoppingItem.ShoppingListId = shoppingList;

        var response = await _httpClient.PostAsJsonAsync("data-api/rest/items", shoppingItem);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteItemAsync(Guid itemId)
    {
        var response = await _httpClient.DeleteAsync($"data-api/rest/items/id/{itemId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task SetItemQuantityAsync(Guid itemId, int? quantity)
    {
        var authState   = await _authStateProvider.GetAuthenticationStateAsync();
        var userIdClaim = authState.User.FindFirst(ClaimTypes.NameIdentifier);

        var response    = await _httpClient.PatchAsJsonAsync($"data-api/rest/items/id/{itemId}",
                new
                {
                    quantity = quantity,
                    userId   = userIdClaim?.Value
                });
        response.EnsureSuccessStatusCode();
    }

    public async Task SetItemImportantAsync(Guid itemId, bool important)
    {
        var authState   = await _authStateProvider.GetAuthenticationStateAsync();
        var userIdClaim = authState.User.FindFirst(ClaimTypes.NameIdentifier);

        var response    = await _httpClient.PatchAsJsonAsync($"data-api/rest/items/id/{itemId}",
                new
                {
                    important = important,
                    userId   = userIdClaim?.Value
                });
        response.EnsureSuccessStatusCode();
    }

    #endregion

}
