namespace Dottor.Blazor.SWA.Functions.Services;

using Dapper;
using Dottor.Blazor.SWA.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class ShoppingService : IShoppingService
{
    private readonly string _connectionString;

    public ShoppingService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ShoppingListDatabase");
    }

    public async Task<IEnumerable<ShoppingItem>> GetItemsAsync(Guid shoppingList)
    {
        const string query = @"
                SELECT [id]
                      ,[shoppingListId]
                      ,[userId]
                      ,[name]
                      ,[quantity]
                      ,[creationDate]
                      ,[important]
                  FROM [dbo].[ShoppingItems]
                  WHERE [shoppingListId] = @shoppingList
                  ORDER BY [Important] DESC, [Name]";

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<ShoppingItem>(query, new { shoppingList });
    }

    public async Task<ShoppingList?> GetShoppingListAsync(Guid shoppingList)
    {
        const string query = @"
                SELECT [id]
                      ,[userId]
                      ,[title]
                      ,[favorite]
                      ,[creationDate]
                  FROM [dbo].[ShoppingLists]
                  WHERE [id] = @shoppingList";

        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<ShoppingList>(query, new { shoppingList });
    }
}