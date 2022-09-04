using System.Reflection;

using NarfuPresentations.Core.Infrastructure.OpenApi.Attributes;

using NJsonSchema;

using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace NarfuPresentations.Core.Infrastructure.OpenApi.Processors;

public class SwaggerHeaderAttributeProcessor : IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        if (context.MethodInfo?.GetCustomAttribute(typeof(SwaggerHeaderAttribute)) is not
            SwaggerHeaderAttribute
            attribute)
            return true;

        var parameters = context.OperationDescription.Operation.Parameters;

        var existingParam = parameters.FirstOrDefault(p =>
            p.Kind == OpenApiParameterKind.Header && p.Name == attribute.HeaderName);
        if (existingParam is not null) parameters.Remove(existingParam);

        parameters.Add(new OpenApiParameter
        {
            Name = attribute.HeaderName,
            Kind = OpenApiParameterKind.Header,
            Description = attribute.Description,
            IsRequired = attribute.IsRequired,
            Schema = new JsonSchema
            {
                Type = JsonObjectType.String,
                Default = attribute.DefaultValue
            }
        });

        return true;
    }
}
