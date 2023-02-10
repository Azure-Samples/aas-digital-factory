using System.Runtime.Serialization;

namespace AasFactory.Azure.Models.Aas.Metamodels.Enums;

/// <summary>
/// This enum is used to define the type of data in data specification.
/// </summary>
public enum DataType
{
    /// <summary>
    /// None type, this is the default type.
    /// </summary>
    None,

    /// <summary>
    /// The data type is date.
    /// </summary>
    [EnumMember(Value = "DATE")]
    Date,

    /// <summary>
    /// The data type is string.
    /// </summary>
    [EnumMember(Value ="STRING")]
    String,

    /// <summary>
    /// The data type is string translatable.
    /// </summary>
    [EnumMember(Value = "STRING_TRANSLATABLE")]
    StringTranslatable,

    /// <summary>
    /// The data type is integer measure.
    /// </summary>
    [EnumMember(Value = "INTEGER_MEASURE")]
    IntegerMeasure,

    /// <summary>
    /// The data type is integer count.
    /// </summary>
    [EnumMember(Value = "INTEGER_COUNT")]
    IntegerCount,

    /// <summary>
    /// The data type is integer currency.
    /// </summary>
    [EnumMember(Value = "INTEGER_CURRENCY")]
    IntegerCurrency,

    /// <summary>
    /// The data type is real measure.
    /// </summary>
    [EnumMember(Value = "REAL_MEASURE")]
    RealMeasure,

    /// <summary>
    /// The data type is real count.
    /// </summary>
    [EnumMember(Value = "REAL_COUNT")]
    RealCount,

    /// <summary>
    /// The data type is real currency.
    /// </summary>
    [EnumMember(Value = "REAL_CURRENCY")]
    RealCurrency,

    /// <summary>
    /// The data type is boolean.
    /// </summary>
    [EnumMember(Value = "BOOLEAN")]
    Boolean,

    /// <summary>
    /// The data type is IRI.
    /// </summary>
    [EnumMember(Value = "IRI")]
    Iri,

    /// <summary>
    /// The data type is IRIDI.
    /// </summary>
    [EnumMember(Value = "IRIDI")]
    IriDi,

    /// <summary>
    /// The data type is rational.
    /// </summary>
    [EnumMember(Value = "RATIONAL")]
    Rational,

    /// <summary>
    /// The data type is rational measure.
    /// </summary>
    [EnumMember(Value = "RATIONAL_MEASURE")]
    RationalMeasure,

    /// <summary>
    /// The data type is time.
    /// </summary>
    [EnumMember(Value = "TIME")]
    Time,

    /// <summary>
    /// The data type is timestamp.
    /// </summary>
    [EnumMember(Value = "TIMESTAMP")]
    Timestamp,

    /// <summary>
    /// The data type is html.
    /// </summary>
    [EnumMember(Value = "HTML")]
    Html,

    /// <summary>
    /// The data type is blob.
    /// </summary>
    [EnumMember(Value = "BLOB")]
    Blob,

    /// <summary>
    /// The data type is file.
    /// </summary>
    [EnumMember(Value = "FILE")]
    File,
}