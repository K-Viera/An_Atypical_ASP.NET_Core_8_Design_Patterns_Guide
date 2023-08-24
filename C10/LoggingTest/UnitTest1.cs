using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoggingTest
{
    public class UnitTest1
    {
        [Fact]
        public void Should_log_the_Service_Execute_line()
        {
            var lines = new List<string>();
            var host = Host.CreateDefaultBuilder()
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddAssertableLogger(lines);
                }).ConfigureServices(services =>
                {
                    services.AddSingleton<Service>();
                }).Build();

            var service = host.Services.GetRequiredService<Service>();
            // Act 
            service.Execute();

            // Assert 
            Assert.Collection(lines,
                line => Assert.Equal("Service.Execute()", line)
            );
        }
        [Fact]
        public void Should_log_the_Service_Execute_line_using_WebApplication()
        {
            //Arrange
            var lines = new List<string>();
            var builder = WebApplication.CreateBuilder();
            builder.Logging.ClearProviders()
                .AddAssertableLogger(lines);
            builder.Services.AddSingleton<IService, Service>();
            var app = builder.Build();
            var service = app.Services.GetRequiredService<IService>();

            // Act 
            service.Execute();
            // Assert 
            Assert.Collection(lines,
                line => Assert.Equal("Service.Execute()", line)
            );
        }
    }
}