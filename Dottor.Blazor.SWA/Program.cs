using Dottor.Blazor.SWA;
using Dottor.Blazor.SWA.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore()
                .AddScoped<AuthenticationStateProvider, StaticWebAppsAuthenticationStateProvider>();

builder.Services.AddScoped<IShoppingService, ShoppingService>();

await builder.Build().RunAsync();
