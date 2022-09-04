namespace NarfuPresentations.Presentation.API.Configurations;

internal static class Startup
{
    internal static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            const string configurationsDirectory = "Configurations";
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddJsonFile($"{configurationsDirectory}/logger.json", false, true)
                .AddJsonFile($"{configurationsDirectory}/logger.{env.EnvironmentName}.json", true,
                    true)
                .AddJsonFile($"{configurationsDirectory}/database.json", false, true)
                .AddJsonFile($"{configurationsDirectory}/database.{env.EnvironmentName}.json", true,
                    true)
                .AddJsonFile($"{configurationsDirectory}/middleware.json", false, true)
                .AddJsonFile($"{configurationsDirectory}/middleware.{env.EnvironmentName}.json",
                    true, true)
                .AddJsonFile($"{configurationsDirectory}/security.json", false, true)
                .AddJsonFile($"{configurationsDirectory}/security.{env.EnvironmentName}.json", true,
                    true)
                .AddJsonFile($"{configurationsDirectory}/openapi.json", false, true)
                .AddJsonFile($"{configurationsDirectory}/openapi.{env.EnvironmentName}.json", true,
                    true)
                .AddJsonFile($"{configurationsDirectory}/password.json", false, true)
                .AddJsonFile($"{configurationsDirectory}/password.{env.EnvironmentName}.json", true,
                    true)
                .AddEnvironmentVariables();
        });
        return host;
    }
}
