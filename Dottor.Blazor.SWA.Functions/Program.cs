
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

var host = new HostBuilder()
        .ConfigureFunctionsWorkerDefaults()
        .ConfigureServices(s =>
        {

        })
        .Build();

await host.RunAsync();
