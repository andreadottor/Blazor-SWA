using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Dottor.Blazor.SWA.Functions
{
    public class ExportPdf
    {
        private readonly ILogger _logger;

        public ExportPdf(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExportPdf>();
        }

        [Function(nameof(ExportPdf))]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions (SWA)! ");

            return response;
        }
    }
}
