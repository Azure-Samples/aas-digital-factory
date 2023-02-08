using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions.Interfaces;

/// <Summary>
/// This interface builds the necessary ids for the different AAS components.
/// </Summary>
public interface IAasIdBuilder
{
    /// <summary>
    /// This function creates the id of the shell AAS component.
    /// </summary>
    /// <param name="modelInstanceType">The model instance type.</param>
    /// <param name="modelInstanceId">The id of the model instance.</param>
    /// <returns>The id of the shell AAS component.</returns>
    string BuildShellId(ModelInstanceType modelInstanceType, string modelInstanceId);

    /// <summary>
    /// This function creates the id of the reference element AAS component.
    /// </summary>
    /// <param name="toShellModelInstance">The model instance type of the target shell.</param>
    /// <param name="toModelInstanceId">The id of the target model instance.</param>
    /// <returns>The id of the reference element AAS component.</returns>
    string BuildReferenceElementId(ModelInstanceType toShellModelInstance, string toModelInstanceId);

    /// <summary>
    /// This function create the id of the reference AAS component.
    /// </summary>
    /// <param name="referenceId">The id of the AAS component that is being referenced.</param>
    /// <returns>The id of the reference AAS component.</returns>
    string BuildReferenceId(string referenceId);

    /// <summary>
    /// This function creates the id of the sub model AAS component.
    /// </summary>
    /// <param name="modelInstanceType">The model instance type.</param>
    /// <param name="modelInstanceId">The id of the model instance.</param>
    /// <param name="subModelType">The type of the sub model.</param>
    /// <returns>The id of the sub model AAS component.</returns>
    string BuildSubModelId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType);

    /// <summary>
    /// This function creates the id of the sub model element AAS component.
    /// </summary>
    /// <param name="modelInstanceType">The model instance type.</param>
    /// <param name="modelInstanceId">The id of the model instance.</param>
    /// <param name="subModelType">The type of the sub model.</param>
    /// <param name="fieldId">The id of the field that represents the sub model element.</param>
    /// <returns>The id of the sub model element AAS component.</returns>
    string BuildSubModelElementId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType, string fieldId);

    /// <summary>
    /// This function creates the id of the sub model element collection AAS component.
    /// </summary>
    /// <param name="modelInstanceType">The model instance type.</param>
    /// <param name="modelInstanceId">The id of the model instance.</param>
    /// <param name="subModelType">The type of the sub model.</param>
    /// <returns>The id of the sub model element collection AAS component.</returns>
    string BuildSubModelElementCollectionId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType);

    /// <summary>
    /// This function creates the id of the sub model element collection AAS component.
    /// </summary>
    /// <param name="modelInstanceType">The model instance type.</param>
    /// <param name="modelInstanceId">The id of the model instance.</param>
    /// <param name="subModelType">The type of the sub model.</param>
    /// <param name="fieldId">The id of the field that represents the sub model element.</param>
    /// <returns>The id of the sub model element collection AAS component.</returns>
    string BuildSubModelElementCollectionFieldId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType, string fieldId);

    /// <summary>
    /// This function creates the id of the sub model element list AAS component.
    /// </summary>
    /// <param name="modelInstanceType">The model instance type.</param>
    /// <param name="modelInstanceId">The id of the model instance.</param>
    /// <param name="subModelType">The type of the sub model.</param>
    /// <param name="elementId">The id of the field that represents the sub model element.</param>
    /// <param name="ListId">The id which can be a field id or an abbreviation to represent the list content.</param>
    /// <returns>The id of the sub model element list AAS component.</returns>
    string BuildSubModelElementListId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType, string elementId, string listId);

    /// <summary>
    /// This function creates the id of the concept description AAS component.
    /// </summary>
    /// <param name="modelInstanceType">The model instance type.</param>
    /// <param name="modelInstanceId">The id of the model instance.</param>
    /// <param name="subModelType">The type of the sub model.</param>
    /// <param name="fieldId">The id of the field that represents the sub model element.</param>
    /// <returns>The id of the concept description AAS component.</returns>
    string BuildConceptDescriptionId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType, string fieldId);

    /// <summary>
    /// This function creates the id of the data specification AAS component.
    /// </summary>
    /// <param name="modelInstanceType">The model instance type.</param>
    /// <param name="modelInstanceId">The id of the model instance.</param>
    /// <param name="subModelType">The type of the sub model.</param>
    /// <param name="fieldId">The id of the field that represents the sub model element.</param>
    /// <returns>The id of the data specification AAS component.</returns>
    string BuildDataSpecificationId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType, string fieldId);
}