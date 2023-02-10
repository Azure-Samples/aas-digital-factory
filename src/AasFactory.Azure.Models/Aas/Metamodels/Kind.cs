using System.Diagnostics.CodeAnalysis;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using Newtonsoft.Json;

namespace AasFactory.Azure.Models.Aas.Metamodels;

[ExcludeFromCodeCoverage]
public class Kind
{
    /// <summary>
    /// Gets or sets the KindValue.
    /// </summary>
    [JsonProperty("kind")]
    public KindType KindValue { get; set; } = KindType.None;
}
