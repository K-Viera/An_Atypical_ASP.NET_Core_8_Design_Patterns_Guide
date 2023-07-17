using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args); 
var app = builder.Build(); 
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

app.Run();

