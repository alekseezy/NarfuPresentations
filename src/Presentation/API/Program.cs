using FluentValidation.AspNetCore;

using NarfuPresentations.Core.Application;
using NarfuPresentations.Core.Infrastructure;
using NarfuPresentations.Core.Infrastructure.Common.Loggers;
using NarfuPresentations.Presentation.API.Configurations;

using Serilog;

namespace NarfuPresentations.Presentation.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            StaticLogger.EnsureInitialized();
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.AddConfigurations();
                builder.Host.UseSerilog((_, config) =>
                {
                    config.WriteTo.Console()
                        .ReadFrom.Configuration(builder.Configuration);
                });

                builder.Services.AddControllers().AddFluentValidation();
                builder.Services.AddInfrastructure(builder.Configuration);
                builder.Services.AddApplication();

                var app = builder.Build();

                await app.Services.InitializeDatabaseAsync();

                app.UseInfrastructure(builder.Configuration);
                app.MapEndpoints();
                app.Run();
            }
            catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                StaticLogger.EnsureInitialized();
                Log.Fatal(ex, "Unhandled exception");
            }
            finally
            {
                StaticLogger.EnsureInitialized();
                Log.Information("Server Shutting down...");
                Log.CloseAndFlush();
            }
        }
    }
}