namespace Dottor.Blazor.SWA.Services;

using Dottor.Blazor.SWA.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IShoppingService
{
    Task AddItemAsync(Guid shoppingList, ShoppingItem shoppingItem);
    Task DeleteItemAsync(Guid itemId);
    Task SetItemQuantityAsync(Guid itemId, int? quantity);
    Task SetItemImportantAsync(Guid itemId, bool important);
    Task<IEnumerable<ShoppingItem>> GetItemsAsync(Guid shoppingList);


    Task<IEnumerable<ShoppingList>> GetShoppingListsAsync();
    Task<ShoppingList?> GetShoppingListAsync(Guid shoppingList);
    Task InsertShoppingListAsync(ShoppingList shoppingList);
    Task DeleteShoppingListAsync(Guid shoppingList);
    Task SetShoppingListFavoriteAsync(Guid shoppingList, bool favorite);
}