using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

#region Inputs

app.MapGet("minimal-endpoint-inline", () => "GET!");

app.MapGet( 
    "minimal-endpoint-input-route-implicit/{id}",  
    (int id) => $"The id was {id}." 
);

app.MapGet(
    "minimal-endpoint-input-route-explicit/{id}",
    ([FromRoute] int id) => $"The id was {id}."
);

app.MapGet(
   "minimal-endpoint-input-HttpContext/",
   (HttpContext context)
       => context.Response.WriteAsync("HttpContext!")
);

app.MapGet( 
   "minimal-endpoint-input-HttpResponse/", 
   (HttpResponse response) 
       => response.WriteAsync("HttpResponse!") 
);

app.MapGet(
   "minimal-endpoint-input-Coordinate/",
   (Coordinate coordinate) => coordinate
);

app.MapGet(
    "minimal-endpoint-input-Person/",
    (Person person) => person
);
app.MapGet( 
    "minimal-endpoint-input-Person2/", 
    ([AsParameters] Person2 person) => person 
); 

#endregion

#region Outputs

app.MapGet( 
    "minimal-endpoint-output-coordinate/", 
    () => new Coordinate { 
        Latitude = 43.653225, 
        Longitude = -79.383186 
    } 
); 

app.MapGet("minimal-endpoint-output-coordinate-ok1/", () =>
    Results.Ok(new Coordinate {
        Latitude = 43.653225,
        Longitude = -79.383186
    })
);

app.MapGet("minimal-endpoint-output-coordinate-ok2/", () =>
    TypedResults.Ok(new Coordinate {
        Latitude = 43.653225,
        Longitude = -79.383186
    })
);

app.MapGet(
    "multiple-TypedResults/",
    Results<Ok, Conflict> ()
        => Random.Shared.Next(0, 100) % 2 == 0
            ? TypedResults.Ok()
            : TypedResults.Conflict()
);

app.MapGet(
    "multiple-TypedResults-delegate/{number}",
    (int number)=>MultipleResultsDelegate(number)
);

Results<Ok, Conflict> MultipleResultsDelegate(int number)
{
    return number % 2 == 0
        ? TypedResults.Ok()
        : TypedResults.Conflict();
}

#endregion

#region Metadata

var metadataGroup = app
    .MapGroup("minimal-endpoint-metadata")
    .WithTags("Metadata Endpoints")
    .WithOpenApi()
;


const string NamedEndpointName = "Named Endpoint";
metadataGroup
    .MapGet("with-name", () => $"Endpoint with name '{NamedEndpointName}'.")
    .WithName(NamedEndpointName)
    .WithOpenApi(operation =>
    {
        operation.Description = "An endpoint that returns its name.";
        operation.Summary = $"Endpoint named '{NamedEndpointName}'.";
        operation.Deprecated = true;
        return operation;
    });

metadataGroup
    .MapGet("url-of-named-endpoint/{endpointName?}", (string? endpointName, LinkGenerator linker) =>
    {
        var name = endpointName ?? NamedEndpointName;
        return new
        {
            name,
            uri = linker.GetPathByName(name)
        };
    })
    .WithDescription("Return the URL of the specified named endpoint.")
    .WithOpenApi(operation =>
    {
        var endpointName = operation.Parameters[0];
        endpointName.Description = "The name of the endpoint to get the URL for.";
        endpointName.AllowEmptyValue = true;
        endpointName.Example = new OpenApiString(NamedEndpointName);
        return operation;
    });

metadataGroup
    .MapGet("excluded-from-open-api", () => { })
    .ExcludeFromDescription()
;

#endregion

app.Run();

public class Person2 
{ 
    public required string Name { get; set; } 
    public required DateOnly Birthday { get; set; } 
} 

