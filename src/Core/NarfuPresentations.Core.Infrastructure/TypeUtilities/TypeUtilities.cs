using System.Reflection;

namespace NarfuPresentations.Core.Infrastructure.TypeUtilities;

public static class TypeUtilities
{
    public static IEnumerable<T?> GetAllPublicConstantValues<T>(this Type type) where T : class =>
        type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        .Where(field => field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(T))
        .Select(field => field.GetRawConstantValue() as T);
}
