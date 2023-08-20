using Microsoft.Extensions.Options;
using OptionsConfiguration;

var builder = WebApplication.CreateBuilder(args);

const string NamedInstance = "MyNamedInstance";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.PostConfigure<ConfigureMeOptions>(
    NamedInstance,
    x => x.Lines = x.Lines.Append("Inline PostConfigure Before")
);

builder.Services
    .Configure<ConfigureMeOptions>(builder.Configuration.GetSection("configureMe"))
    .Configure<ConfigureMeOptions>(NamedInstance,builder.Configuration.GetSection("configureMe"));

builder.Services.AddSingleton<IConfigureOptions<ConfigureMeOptions>, ConfigureAllConfigureMeOptions>();

builder.Services.AddSingleton<IPostConfigureOptions<ConfigureMeOptions>, ConfigureAllConfigureMeOptions>();

builder.Services.AddSingleton<IConfigureOptions<ConfigureMeOptions>, ConfigureMoreConfigureMeOptions>();

builder.Services.PostConfigure<ConfigureMeOptions>(
    NamedInstance,
    x => x.Lines = x.Lines.Append("Inline PostConfigure After")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/configure-me",(IOptionsMonitor<ConfigureMeOptions> options) =>
new {
    DefaultIstance = options.CurrentValue,
    NamedInstance = options.Get(NamedInstance)
});


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
