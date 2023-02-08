using System.Diagnostics.CodeAnalysis;

namespace AasFactory.Azure.Functions.ModelDataFlow.Parameters;

/// <summary>
/// This class contains parameters used in the conversion of data to Aas.
/// </summary>
[ExcludeFromCodeCoverage]
public static class AasConversionParameters
{
    // property parameters

    /// <summary>
    /// This setting defines the value of the place name property id short.
    /// </summary>
    public const string PropertyPlaceNameIdShort = "PlaceName";

    /// <summary>
    /// This setting defines the value of the time zone property id short.
    /// </summary>
    public const string PropertyTimeZoneIdShort = "TimeZone";

    // concept description parameters

    /// <summary>
    /// This setting defines the value of the concept description id short.
    /// </summary>
    public const string ConceptDescriptionIdShort = "ConceptDescription";

    // data specification parameters

    /// <summary>
    /// This setting defines the value of the data specification id short.
    /// </summary>
    public const string DataSpecificationIdShort = "DataSpecification";

    // Misc parameters

    /// <summary>
    /// This setting defines the value of IRI base url.
    /// </summary>
    public const string IriBaseUrl = "https://aasfactory.com/aas";
}