namespace Dottor.Blazor.SWA.Services
{
    using Dottor.Blazor.SWA.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShoppingService
    {
        Task<IEnumerable<ShoppingList>> GetShoppingListsAsync();
        Task InsertShoppingList(ShoppingList shoppingList);
    }
}