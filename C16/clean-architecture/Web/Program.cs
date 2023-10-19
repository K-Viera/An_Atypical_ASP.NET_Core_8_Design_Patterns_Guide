using Core;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Core.UseCases;
using Infrastructure.Data.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    // Core Layer
    .AddScoped<StockService>()

    // Infrastructure Layer (mapping Core to Infrastructure.Data.EF)
    .AddScoped<IProductRepository, ProductRepository>()
    .AddDbContext<ProductContext>(options => options
        .UseInMemoryDatabase("ProductContextMemoryDB")
        .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
    )
;
builder.Services
    // Core Layer 
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<NotEnoughStockException>())
;

var app = builder.Build();

app.MapGet("/products", async (IProductRepository productRepository, CancellationToken cancellationToken) =>
{
    var products = await productRepository.AllAsync(cancellationToken);
    return products.Select(p => new
    {
        p.Id,
        p.Name,
        p.QuantityInStock
    });
});

app.MapPost("/products/{productId:int}/add-stocks", async (

    int productId,

    AddStocks.Command command,

    IMediator mediator,

    CancellationToken cancellationToken) =>

{

    command.ProductId = productId;

    var quantityInStock = await mediator.Send(command, cancellationToken);

    var stockLevel = new StockLevel(quantityInStock);

    return Results.Ok(stockLevel);

});

app.MapPost("/products/{productId:int}/remove-stocks", async (

    int productId,

    RemoveStocks.Command command,

    IMediator mediator,

    CancellationToken cancellationToken) =>

{

    command.ProductId = productId;

    var quantityInStock = await mediator.Send(command, cancellationToken);

    var stockLevel = new StockLevel(quantityInStock);

    return Results.Ok(stockLevel);

});

using (var seedScope = app.Services.CreateScope())
{
    var db = seedScope.ServiceProvider.GetRequiredService<ProductContext>();
    await ProductSeeder.SeedAsync(db);
}
app.Run();

internal class AddStocksCommand
{
    public int Amount { get; set; }
}

internal class RemoveStocksCommand
{
    public int Amount { get; set; }
}

public record class StockLevel(int QuantityInStock);

internal static class ProductSeeder
{
    public static Task SeedAsync(ProductContext db)
    {
        db.Products.Add(new Product(
            id: 1,
            name: "Banana",
            quantityInStock: 50
        ));
        db.Products.Add(new Product(
            id: 2,
            name: "Apple",
            quantityInStock: 20
        ));
        db.Products.Add(new Product(
            id: 3,
            name: "Habanero Pepper",
            quantityInStock: 10
        ));
        return db.SaveChangesAsync();
    }
}