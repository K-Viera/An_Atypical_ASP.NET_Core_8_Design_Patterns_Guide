using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddSingleton<OperationResultPatterns.SimplestForm.Executor>()
    .AddSingleton<OperationResultPatterns.SingleError.Executor>()
    .AddSingleton<OperationResultPatterns.SingleErrorWithValue.Executor>()
    .AddSingleton<OperationResultPatterns.MultipleErrorsWithValue.Executor>()
    .AddSingleton<OperationResultPatterns.WithSeverity.Executor>()
    .AddSingleton<OperationResultPatterns.StaticFactoryMethod.Executor>()
    .Configure<JsonOptions>(o
        => o.SerializerOptions.Converters.Add(
            new JsonStringEnumConverter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/simplest-form",
    (OperationResultPatterns.SimplestForm.Executor executor) =>
    {
        var result = executor.Operation();
        if (result.Succeeded) return "Operation succeeded";
        else return "Operation failed";
    });

app.MapGet(
    "/single-error",
    (OperationResultPatterns.SingleError.Executor executor) =>
    {
        var result = executor.Operation();
        if (result.Succeeded) return "Operation succeeded";
        else return result.ErrorMessage;
    }
);

app.MapGet(
    "/single-error-with-value",
    (OperationResultPatterns.SingleErrorWithValue.Executor executor) =>
    {
        var result = executor.Operation();
        if (result.Succeeded)
            return $"Operation succeeded with a value of '{result.Value}'.";
        else
            return result.ErrorMessage;
    }
);

app.MapGet("/multiple-errors-with-value",
    object (OperationResultPatterns.MultipleErrorsWithValue.Executor executor) =>
    {
        var result = executor.Operation();
        if (result.Succeeded) return $"Operation succeeded with a value of '{result.Value}'.";
        else return result.Errors;
    });

app.MapGet("multiple-errors-with-value-and-severity",
    (OperationResultPatterns.WithSeverity.Executor executor) =>
    {
        var result = executor.Operation();
        if (result.Succeeded)
        {
            // Handle the success
        }
        else
        {
            // Handle the failure
        }
        return result;
    });

app.Run();

