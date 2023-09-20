namespace Dottor.Blazor.SWA.Functions.Utilities;

using Dottor.Blazor.SWA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Text;
using System.Text.Json;

internal static class StaticWebApiAppAuthorization
{
    public static ClientPrincipal ParseHttpHeaderForClientPrinciple(HttpRequestData req)
{
    if (!req.Headers.TryGetValues("x-ms-client-principal", out var header))
    {
        return new ClientPrincipal();
    }

    var data = header.First();
    var decoded = Convert.FromBase64String(data);
    var json = Encoding.UTF8.GetString(decoded);
    var principal = JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return principal ?? new ClientPrincipal();
}
}
