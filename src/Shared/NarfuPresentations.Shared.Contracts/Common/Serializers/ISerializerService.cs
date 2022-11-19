namespace NarfuPresentations.Shared.Contracts.Common.Serializers;

public interface ISerializerService
{
    string Serialize<T>(T obj);
    string Serialize<T>(T obj, Type type);
    T? Deserialize<T>(string text);
}
