namespace Dottor.Blazor.SWA.Functions.Services;

using Dottor.Blazor.SWA.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IShoppingService
{
    Task<IEnumerable<ShoppingItem>> GetItemsAsync(Guid shoppingList);
    Task<ShoppingList?> GetShoppingListAsync(Guid shoppingList);

}
