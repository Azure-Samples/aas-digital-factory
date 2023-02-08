using System.Reflection;
using System.Runtime.Serialization;

namespace AasFactory.Models.Enums;

/// <summary>
/// Extension class for Enum types.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Get the value associated with the EnumMember.Value attribute.
    /// </summary>
    /// <param name="value">the enum.</param>
    /// <returns>EnumMember.Value for the enum or null.</returns>
    public static string? GetEnumMemberValue<T>(this T value)
        where T : Enum
    {
        return typeof(T)
            .GetTypeInfo()
            .DeclaredMembers
            .SingleOrDefault(x => x.Name == value.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>(false)
            ?.Value;
    }
}