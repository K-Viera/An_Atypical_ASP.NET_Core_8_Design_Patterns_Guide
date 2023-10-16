using Core;
using Core.Mappers;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services// Web Layer
    .AddSingleton<IMapper<Product, ProductDetails>, ProductMapper>()
    .AddSingleton<IMapper<ProductNotFoundException, ProductNotFound>, ExceptionsMapper>()
    .AddSingleton<IMapper<NotEnoughStockException, NotEnoughStock>, ExceptionsMapper>()
    .AddSingleton<IMappingService, ServiceLocatorMappingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", async (
    IProductRepository productRepository,
    IMapper<Product, ProductDetails> mapper,
    CancellationToken cancellationToken) =>
{
    var products = await productRepository.AllAsync(cancellationToken);
    return products.Select(p => mapper.Map(p));
});


app.MapPost("/products/{productId:int}/remove-stocks", async (int productId, RemoveStocksCommand command, StockService stockService, IMappingService mapper, CancellationToken cancellationToken) =>
{
    try
    {
        var quantityInStock = await stockService.RemoveStockAsync(productId, command.Amount, cancellationToken);
        var stockLevel = new StockLevel(quantityInStock);
        return Results.Ok(stockLevel);
    }
    catch (NotEnoughStockException ex)
    {
        return Results.Conflict(mapper.Map<NotEnoughStockException, NotEnoughStock>(ex));
    }
    catch (ProductNotFoundException ex)
    {
        return Results.NotFound(mapper.Map<ProductNotFoundException, ProductNotFound>(ex));
    }
}).Produces(200, typeof(StockLevel))
  .Produces(404, typeof(ProductNotFound))
  .Produces(409, typeof(NotEnoughStock));


app.Run();

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

public record class AddStocksCommand(int Amount);
public record class RemoveStocksCommand(int Amount);

public record class ProductDetails(int Id, string Name, int QuantityInStock);
public class ProductMapper : IMapper<Product, ProductDetails>
{
    public ProductDetails Map(Product entity)
        => new(entity.Id ?? default, entity.Name, entity.QuantityInStock);
}

public record class ProductNotFound(int ProductId, string Message);
public record class NotEnoughStock(int AmountToRemove, int QuantityInStock, string Message);
public class ExceptionsMapper : IMapper<ProductNotFoundException, ProductNotFound>, IMapper<NotEnoughStockException, NotEnoughStock>
{
    public ProductNotFound Map(ProductNotFoundException exception)
        => new(exception.ProductId, exception.Message);
    public NotEnoughStock Map(NotEnoughStockException exception)
        => new(exception.AmountToRemove, exception.QuantityInStock, exception.Message);
}

public record class StockLevel(int QuantityInStock);

