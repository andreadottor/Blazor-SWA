using Dottor.Blazor.SWA.Functions.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var host = new HostBuilder()
        .ConfigureFunctionsWorkerDefaults()
        .ConfigureServices(s =>
        {
            s.AddScoped<IShoppingService, ShoppingService>();
        })
        .Build();

await host.RunAsync();
