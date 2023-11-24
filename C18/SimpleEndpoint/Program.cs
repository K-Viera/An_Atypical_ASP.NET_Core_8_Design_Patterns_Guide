using SimpleEndpoint;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ShuffleText.Endpoint>();
builder.Services.AddSingleton<RandomNumber.Handler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/shuffle-text/{text}", ([AsParameters] ShuffleText.Request query, ShuffleText.Endpoint endpoint)
    => endpoint.Handle(query) 
    );


app.MapGet("/random-number/{Amount}/{Min}/{Max}", RandomNumber.Endpoint);

app.Run();
