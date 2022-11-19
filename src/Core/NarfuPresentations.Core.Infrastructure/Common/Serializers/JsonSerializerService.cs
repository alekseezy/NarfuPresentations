using NarfuPresentations.Core.Application.Common.Serializers;

using System.Text.Json;

namespace NarfuPresentations.Core.Infrastructure.Common.Serializers;

public class JsonSerializerService : ISerializerService
{
    public T? Deserialize<T>(string text)
    {
        return JsonSerializer.Deserialize<T>(text);
    }

    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public string Serialize<T>(T obj, Type type)
    {
        return JsonSerializer.Serialize(obj, type);
    }
}
