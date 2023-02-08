using System.Runtime.Serialization;

namespace AasFactory.Azure.Models.Aas.Metamodels.Enums;

/// <summary>
/// This enum is used to define the type of the sub model elements contained in the list.
/// </summary>
public enum TypeValueListElement
{
    /// <summary>
    /// None type, this is the default type.
    /// </summary>
    None,

    /// <summary>
    /// The sub model element type in the list is annotated relationship element.
    /// </summary>
    AnnotatedRelationshipElement,

    /// <summary>
    /// The sub model element type in the list is basic event element.
    /// </summary>
    BasicEventElement,

    /// <summary>
    /// The sub model element type in the list is blob.
    /// </summary>
    Blob,

    /// <summary>
    /// The sub model element type in the list is capability.
    /// </summary>
    Capability,

    /// <summary>
    /// The sub model element type in the list is data element.
    /// </summary>
    DataElement,

    /// <summary>
    /// The sub model element type in the list is entity.
    /// </summary>
    Entity,

    /// <summary>
    /// The sub model element type in the list is event element.
    /// </summary>
    EventElement,

    /// <summary>
    /// The sub model element type in the list is file.
    /// </summary>
    File,

    /// <summary>
    /// The sub model element type in the list is multi language property.
    /// </summary>
    MultiLanguageProperty,

    /// <summary>
    /// The sub model element type in the list is operation.
    /// </summary>
    Operation,

    /// <summary>
    /// The sub model element type in the list is property.
    /// </summary>
    Property,

    /// <summary>
    /// The sub model element type in the list is range.
    /// </summary>
    Range,

    /// <summary>
    /// The sub model element type in the list is reference element.
    /// </summary>
    ReferenceElement,

    /// <summary>
    /// The sub model element type in the list is relationship element.
    /// </summary>
    RelationshipElement,

    /// <summary>
    /// The sub model element type in the list is sub model element.
    /// </summary>
    [EnumMember(Value = "SubmodelElement")]
    SubModelElement,

    /// <summary>
    /// The sub model element type in the list is sub model element collection.
    /// </summary>
    [EnumMember(Value = "SubmodelElementCollection")]
    SubModelElementCollection,

    /// <summary>
    /// The sub model element type in the list is sub model element list.
    /// </summary>
    [EnumMember(Value = "SubmodelElementList")]
    SubModelElementList,
}