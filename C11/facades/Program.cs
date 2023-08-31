using OpaqueFacadeSubSystem;
using TransparentFacadeSubSystem;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddOpaqueFacadeSubSystem()
    .AddTransparentFacadeSubSystem();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet(
    "/opaque/a",
    (IOpaqueFacade opaqueFacade)
        => opaqueFacade.ExecuteOperationA()
);

app.MapGet(
    "/opaque/b",
    (IOpaqueFacade opaqueFacade)
        => opaqueFacade.ExecuteOperationB()
);

app.MapGet(
    "/transparent/a",
    (ITransparentFacade transparentFacade)
        => transparentFacade.ExecuteOperationA()
);
app.MapGet(
    "/transparent/b",
    (ITransparentFacade transparentFacade)
        => transparentFacade.ExecuteOperationB()
);

app.UseHttpsRedirection();

app.Run();

