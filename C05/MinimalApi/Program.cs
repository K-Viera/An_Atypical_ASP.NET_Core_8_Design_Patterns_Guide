using System.Text.Json;

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

app.MapJsonSerializationEndpoints();

app.MapInlineGroupEndpoints();

app.Run();

public class Person2
{
    public required string Name { get; set; }
    public required DateOnly Birthday { get; set; }
}

