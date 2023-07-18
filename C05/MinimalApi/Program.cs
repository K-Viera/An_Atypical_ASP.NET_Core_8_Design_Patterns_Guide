using System.Text.Json;
using Shared.Models;
using Shared.Data; 
using Shared.DTO

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapInputsEndpoints();

app.MapOutputEndpoints();

app.MapMetadataEndpoints();

app.MapInlineGroupEndpoints().MapJsonSerializationEndpoints();

var group = app.MapOrganizingEndpointsComposable();

group.MapPut("/{customerId}", async (int customerId, Customer input, ICustomerRepository customerRepository, CancellationToken cancellationToken) =>
{
    var updatedCustomer = await customerRepository.UpdateAsync(
        input,
        cancellationToken
    );
    if (updatedCustomer == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(updatedCustomer);
});

// // var group = app 
// //     .MapGroup("/dto/customers") 
// //     .WithTags("Customer DTO") 
// //     .WithOpenApi() 
// // ; 

// group.MapGet("/", GetCustomersSummaryAsync)
//     .WithName("GetAllCustomersSummary");

// group.MapGet("/{customerId}", GetCustomerDetailsAsync)
//     .WithName("GetCustomerDetailsById");

// group.MapPut("/{customerId}", UpdateCustomerAsync)
//     .WithName("UpdateCustomerWithDto");

// group.MapPost("/", CreateCustomerAsync)
//     .WithName("CreateCustomerWithDto");

// group.MapDelete("/{customerId}", DeleteCustomerAsync)
//     .WithName("DeleteCustomerWithDto");

app.Run();

