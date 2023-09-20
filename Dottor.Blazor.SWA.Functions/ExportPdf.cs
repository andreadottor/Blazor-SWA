using System.Collections.Generic;
using System.Data.Common;
using System.Net;
using Dottor.Blazor.SWA.Functions.Services;
using Dottor.Blazor.SWA.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Dottor.Blazor.SWA.Functions;

public class ExportPdf
{
    private readonly ILogger _logger;
    private readonly IShoppingService _shoppingService;

    public ExportPdf(ILoggerFactory loggerFactory, IShoppingService shoppingService)
    {
        _logger = loggerFactory.CreateLogger<ExportPdf>();
        _shoppingService = shoppingService;
    }

    [Function(nameof(ExportPdf))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var idArg = req.Query["id"];
        if (string.IsNullOrWhiteSpace(idArg))
            return req.CreateResponse(HttpStatusCode.BadRequest);

        var shoppingListId = Guid.Parse(idArg);
        var shoppingList = await _shoppingService.GetShoppingListAsync(shoppingListId);
        if(shoppingList is null)
            return req.CreateResponse(HttpStatusCode.NotFound);

        var items = await _shoppingService.GetItemsAsync(shoppingListId);
        var file = Generate(shoppingList, items);
        var fileName = GetFileName(shoppingList.Title);

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/octet-stream");
        response.Headers.Add("content-disposition", $"attachment;filename={fileName}");

        response.WriteBytes(file);

        return response;
    }

    private string GetFileName(string title)
    {
        var temp = title.Replace(" ", "_");
        foreach(char c in System.IO.Path.GetInvalidFileNameChars()) {
            temp = temp.Replace(c, '_');
        }

        return $"ShoppingList-{temp}.pdf";
    }


    private byte[] Generate(ShoppingList shoppingList, IEnumerable<ShoppingItem> items)
    {
        // code in your main method
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(18));

                page.Header()
                        .Text(shoppingList.Title)
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            //column.Spacing(20);

                            foreach (var item in items)
                            {
                                column.Item().Row(row =>
                                {
                                    row.AutoItem().Text("- ");
                                    row.RelativeItem().Text(item.Name);
                                    row.RelativeItem().Text(item.Quantity?.ToString() ?? "");
                                    row.RelativeItem().Text(item.Important ? "!!" : "");
                                });
                            }

                        });

                page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
            });
        });
        return document.GeneratePdf();
    }
}
