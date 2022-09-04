using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NarfuPresentations.Core.Infrastructure.OpenApi.Processors;
using NarfuPresentations.Core.Infrastructure.OpenApi.Settings;

using NJsonSchema;
using NJsonSchema.Generation.TypeMappers;

using NSwag;
using NSwag.Generation.Processors.Security;

namespace NarfuPresentations.Core.Infrastructure.OpenApi;

internal static class Startup
{
    internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();

        if (!settings.Enable)
            return services;

        services.AddVersionedApiExplorer(options => options.SubstituteApiVersionInUrl = true);
        services.AddEndpointsApiExplorer();
        services.AddOpenApiDocument((document, _) =>
        {
            document.PostProcess = doc =>
            {
                doc.Info.Title = settings.Title;
                doc.Info.Version = settings.Version;
                doc.Info.Description = settings.Description;
                doc.Info.Contact = new OpenApiContact
                {
                    Name = settings.ContactName,
                    Email = settings.ContactEmail,
                    Url = settings.ContactUrl
                };
                doc.Info.License = new OpenApiLicense
                {
                    Name = settings.LicenseName,
                    Url = settings.LicenseUrl
                };
            };

            document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Input your Bearer token to access this API.",
                In = OpenApiSecurityApiKeyLocation.Header,
                Type = OpenApiSecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            });

            document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor());
            document.OperationProcessors.Add(new SwaggerGlobalAuthProcessor());

            document.TypeMappers.Add(new PrimitiveTypeMapper(typeof(TimeSpan), schema =>
            {
                schema.Type = JsonObjectType.String;
                schema.IsNullableRaw = true;
                schema.Pattern =
                    @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$";
                schema.Example = "02:00:00";
            }));

            document.OperationProcessors.Add(new SwaggerHeaderAttributeProcessor());
        });

        return services;
    }

    internal static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app,
        IConfiguration configuration)
    {
        if (!configuration.GetValue<bool>("SwaggerSettings:Enable"))
            return app;

        app.UseOpenApi();
        app.UseSwaggerUi3(options =>
        {
            options.DefaultModelsExpandDepth = -1;
            options.DocExpansion = "none";
            options.TagsSorter = "alpha";
        });

        return app;
    }
}
