namespace Dottor.Blazor.SWA.Services
{
    using Dottor.Blazor.SWA.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShoppingService
    {
        Task AddItem(Guid shoppingList, ShoppingItem shoppingItem);
        Task<IEnumerable<ShoppingItem>> GetItemsAsync(Guid shoppingList);

        Task<IEnumerable<ShoppingList>> GetShoppingListsAsync();
        Task<ShoppingList?> GetShoppingListAsync(Guid shoppingList);
        Task InsertShoppingListAsync(ShoppingList shoppingList);
        Task DeleteShoppingListAsync(Guid shoppingList);
        Task ShoppingListSetFavoriteAsync(Guid shoppingList, bool favorite);
    }
}