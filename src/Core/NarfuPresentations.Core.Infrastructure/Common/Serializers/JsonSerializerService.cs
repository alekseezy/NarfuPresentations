using System.Text.Json;

using NarfuPresentations.Core.Application.Common.Serializers;

namespace NarfuPresentations.Core.Infrastructure.Common.Serializers;

public class JsonSerializerService : ISerializerService
{
    public T? Deserialize<T>(string text) => JsonSerializer.Deserialize<T>(text);

    public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj);

    public string Serialize<T>(T obj, Type type) => JsonSerializer.Serialize(obj, type);
}
