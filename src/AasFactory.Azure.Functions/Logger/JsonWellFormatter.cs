using Newtonsoft.Json;

namespace AasFactory.Azure.Functions.Logger;

/// <summary>
/// This class will override the behavior of ToString function for any template type to return a json string instead of the type.
/// </summary>
/// <typeparam name="T">The template type that the behavior will be overridden</typeparam>
public class JsonWellFormatter<T>
{
    private T baseObject;

    public JsonWellFormatter(T baseObject)
    {
        this.baseObject = baseObject;
    }

    /// <summary>
    /// Will implicitly convert an object to a string with json format.
    /// </summary>
    /// <param name="baseObject">an object from any type to convert to json string</param>
    /// <typeparam name="T">The object type</typeparam>
    public static implicit operator JsonWellFormatter<T>(T baseObject)
    {
        return new JsonWellFormatter<T>(baseObject);
    }

    /// <summary>
    /// convert any object to standard json formatted string.
    /// </summary>
    /// <returns></returns>
    public static string ToString(T baseObject)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",
        };

        return JsonConvert.SerializeObject(baseObject, Formatting.Indented, settings).Trim('"');
    }

    /// <summary>
    /// Overriding the default behavior for ToString function which is usually returns the object type to return a json-formatted string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return ToString(this.baseObject);
    }
}