using CommonScenarios;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder where able to get it from appsettings by calling WebApplication.CreateBuilder(args);
var defaultOptionsSection = builder.Configuration.GetSection("defaultOptions");

//builder.Services.Configure<MyOptions>(myOptions =>
//{
//    myOptions.Name = "Default Option";
//});
builder.Services.Configure<MyOptions>(defaultOptionsSection);
builder.Services.Configure<MyOptions>(
    "Options1",
    builder.Configuration.GetSection("options1"));
builder.Services.Configure<MyOptions>(
    "Options2",
    builder.Configuration.GetSection("options2"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/my-options/",
    (IOptions<MyOptions> options)=> options.Value);

app.MapGet(
    "/factory/{name}",
    (string name,IOptionsFactory<MyOptions> factory) => factory.Create(name));

app.MapGet("/monitor/{name}",(string name,IOptionsMonitor<MyOptions> monitor)=> monitor.Get(name));

app.MapGet(
    "/snapshot",
    (IOptionsSnapshot<MyOptions> snapshot)
        => snapshot.Value
);

app.MapGet(
    "/snapshot/{name}",
    (string name, IOptionsSnapshot<MyOptions> snapshot)
        => snapshot.Get(name)
);
app.Run();

public class MyOptions
{
    public string? Name { get; set; }
}
