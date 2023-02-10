using System.Runtime.Serialization;

namespace AasFactory.Azure.Models.Aas.Metamodels.Enums;

/// <summary>
/// This enum is used to define the type of key.
/// </summary>
public enum KeyType
{
    /// <summary>
    /// None type, this is the default type.
    /// </summary>
    None,

    /// <summary>
    /// The key is for the referable AAS type.
    /// </summary>
    [EnumMember(Value = "Referable")]
    Referable,

    /// <summary>
    /// The key is for the fragment reference AAS type.
    /// </summary>
    [EnumMember(Value ="FragmentReference")]
    FragmentReference,

    /// <summary>
    /// The key is for the global reference AAS type.
    /// </summary>
    [EnumMember(Value = "GlobalReference")]
    GlobalReference,

    /// <summary>
    /// The key is for the asset administration shell AAS type.
    /// </summary>
    [EnumMember(Value = "AssetAdministrationShell")]
    AssetAdministrationShell,

    /// <summary>
    /// The key is for the concept description AAS type.
    /// </summary>
    [EnumMember(Value = "ConceptDescription")]
    ConceptDescription,

    /// <summary>
    /// The key is for the identifiable AAS type.
    /// </summary>
    [EnumMember(Value = "Identifiable")]
    Identifiable,

    /// <summary>
    /// The key is for the sub model AAS type.
    /// </summary>
    [EnumMember(Value = "Submodel")]
    SubModel,

    /// <summary>
    /// The key is for the annotated relationship element AAS type.
    /// </summary>
    [EnumMember(Value = "AnnotatedRelationshipElement")]
    AnnotatedRelationshipElement,

    /// <summary>
    /// The key is for the basic event element AAS type.
    /// </summary>
    [EnumMember(Value = "BasicEventElement")]
    BasicEventElement,

    /// <summary>
    /// The key is for the blob AAS type.
    /// </summary>
    [EnumMember(Value = "Blob")]
    Blob,

    /// <summary>
    /// The key is for the capability AAS type.
    /// </summary>
    [EnumMember(Value = "Capability")]
    Capability,

    /// <summary>
    /// The key is for the data element AAS type.
    /// </summary>
    [EnumMember(Value = "DataElement")]
    DataElement,

    /// <summary>
    /// The key is for the entity AAS type.
    /// </summary>
    [EnumMember(Value = "Entity")]
    Entity,

    /// <summary>
    /// The key is for the event element AAS type.
    /// </summary>
    [EnumMember(Value = "EventElement")]
    EventElement,

    /// <summary>
    /// The key is for the file AAS type.
    /// </summary>
    [EnumMember(Value = "File")]
    File,

    /// <summary>
    /// The key is for the multi language property AAS type.
    /// </summary>
    [EnumMember(Value = "MultiLanguageProperty")]
    MultiLanguageProperty,

    /// <summary>
    /// The key is for the operation AAS type.
    /// </summary>
    [EnumMember(Value = "Operation")]
    Operation,

    /// <summary>
    /// The key is for the property AAS type.
    /// </summary>
    [EnumMember(Value = "Property")]
    Property,

    /// <summary>
    /// The key is for the range AAS type.
    /// </summary>
    [EnumMember(Value = "Range")]
    Range,

    /// <summary>
    /// The key is for the reference element AAS type.
    /// </summary>
    [EnumMember(Value = "ReferenceElement")]
    ReferenceElement,

    /// <summary>
    /// The key is for the relationship element AAS type.
    /// </summary>
    [EnumMember(Value = "RelationshipElement")]
    RelationshipElement,

    /// <summary>
    /// The key is for the sub model element AAS type.
    /// </summary>
    [EnumMember(Value = "SubmodelElement")]
    SubModelElement,

    /// <summary>
    /// The key is for the sub model element collection AAS type.
    /// </summary>
    [EnumMember(Value = "SubmodelElementCollection")]
    SubModelElementCollection,

    /// <summary>
    /// The key is for the sub model element list AAS type.
    /// </summary>
    [EnumMember(Value = "SubmodelElementList")]
    SubModelElementList,
}