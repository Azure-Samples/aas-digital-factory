using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory;
using AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces.AasConverters;

public interface IConverterHelpers
{
    /// <summary>
    /// Creates an IRI for the shell or the sub model.
    /// </summary>
    /// <param name="assetKind">The asset kind type.</param>
    /// <param name="id">The dtid of the shell or sub model.</param>
    /// <param name="isShell">Boolean to indicate if the component is a shell.</param>
    /// <returns>The Iri for the shell or the sub model.</returns>
    public string CreateIri(AssetKindType assetKind, string id, bool isShell = true);

    /// <summary>
    /// Creates a list of <see cref="ReferenceElement" />.
    /// </summary>
    /// <param name="toType">The type of model instance being referenced.</param>
    /// <param name="identifiableInstances">The list of <see cref="IdentifiableInstance" />.</param>
    /// <param name="kind">The kind of the reference elements.</param>
    /// <returns>A list of <see cref="ReferenceElement" />.</returns>
    public IEnumerable<ReferenceElement> CreateReferenceElementsFrom(
        ModelInstanceType toType,
        IEnumerable<IdentifiableInstance> identifiableInstances,
        KindType kind);

    /// <summary>
    /// Creates a dictionary of sub model types and machine types fields.
    /// </summary>
    /// <param name="machineType">The machine type.</param>
    /// <returns>A dicitonary of machine type fields organized by sub model type.</returns>
    public IDictionary<SubModelType, IList<MachineTypeField>> OrganizeMachineTypeFields(MachineType machineType);

    /// <summary>
    /// Checks to see if the field needs a concept description element.
    /// </summary>
    /// <param name="field">The machine type field.</param>
    /// <returns>A boolean indicating if the field can create a concept description.</returns>
    public bool FieldCanCreateConceptDescription(MachineTypeField field);

    /// <summary>
    /// Builds the sub models and the corresponding properties for the sub models.
    /// </summary>
    /// <param name="fieldsDictionary">A dictionary of sub model types and the corresponding fields for the sub model types.</param>
    /// <param name="modelInstanceType">The model instance type.</param>
    /// <param name="modelInstanceId">The model instance Id.</param>
    /// <param name="kindType">The kind type.</param>
    /// <param name="assetKindType">The asset kind type.</param>
    /// <returns>A list of <see cref="SubModel" />.</returns>
    public IEnumerable<SubModel> BuildSubModels(
        IDictionary<SubModelType, IList<MachineTypeField>> fieldsDictionary,
        ModelInstanceType modelInstanceType,
        string modelInstanceId,
        KindType kindType,
        AssetKindType assetKindType);
}
