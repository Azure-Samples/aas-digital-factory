using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AasFactory.Azure.Models.Adt.Components;
using AasFactory.Models.Enums;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Twins
{
    [ExcludeFromCodeCoverage]
    public class DataSpecification : BasicDigitalTwin
    {
        /// <summary>
        /// Constructor to initialize AAS Data Specification twin
        /// </summary>
        public DataSpecification()
        {
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.DataSpecificationModelId };
        }

        /// <summary>
        /// Constructor to initialize AAS Data Specification twin
        /// </summary>
        /// <param name="dataSpecification">The AAS Data Specification ontology</param>
        public DataSpecification(Aas.Metamodels.DataSpecification dataSpecification)
        {
            this.Id = dataSpecification.Id;
            this.Definition = new LangStringSet(dataSpecification.Definition);
            this.PreferredName = new LangStringSet(dataSpecification.PreferredName);
            this.ShortName = new LangStringSet(dataSpecification.ShortName);
            this.DataType = dataSpecification.DataType.GetEnumMemberValue<Aas.Metamodels.Enums.DataType>();
            this.LevelType = dataSpecification.LevelType.GetEnumMemberValue<Aas.Metamodels.Enums.LevelType>();
            this.SourceOfDefinition = dataSpecification.SourceOfDefinition;
            this.Symbol = dataSpecification.Symbol;
            this.Unit = dataSpecification.Unit;
            this.UnitIdValue = dataSpecification.UnitIdValue;
            this.Value = dataSpecification.Value;
            this.ValueFormat = dataSpecification.ValueFormat;
            this.Metadata = new DigitalTwinMetadata { ModelId = AdtConstants.DataSpecificationModelId };
        }

        /// <summary>
        /// Gets or sets the Definition.
        /// </summary>
        [JsonPropertyName("definition")]
        public LangStringSet Definition { get; set; } = new ();

        /// <summary>
        /// Gets or sets the Preferred Name.
        /// </summary>
        [JsonPropertyName("preferredName")]
        public LangStringSet PreferredName { get; set; } = new ();

        /// <summary>
        /// Gets or sets the Short Name.
        /// </summary>
        [JsonPropertyName("shortName")]
        public LangStringSet ShortName { get; set; } = new ();

        /// <summary>
        /// Gets or sets the Data Type.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("dataType")]
        public string? DataType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Level Type.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("levelType")]
        public string? LevelType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Source of Definition.
        /// </summary>
        [JsonPropertyName("sourceOfDefinition")]
        public string SourceOfDefinition { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Symbol.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Unit.
        /// </summary>
        [JsonPropertyName("unit")]
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Unit Id Value.
        /// </summary>
        [JsonPropertyName("unitIdValue")]
        public string UnitIdValue { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Value Format.
        /// </summary>
        [JsonPropertyName("valueFormat")]
        public string ValueFormat { get; set; } = string.Empty;
    }
}