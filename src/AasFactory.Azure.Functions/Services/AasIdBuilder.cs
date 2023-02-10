using AasFactory.Azure.Functions.Interfaces;
using AasFactory.Azure.Models.Aas.Metamodels.Enums;
using AasFactory.Azure.Models.Factory.Enums;

namespace AasFactory.Azure.Functions.Services;

/// <summary>
/// This class builds the AAS Ids for the necessary components.
/// </summary>
public class AasIdBuilder : IAasIdBuilder
{
    private const string IdPrefix = "aas";

    /// <inheritdoc />
    public string BuildConceptDescriptionId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType, string fieldId)
    {
        var abbreviatedModelInstanceType = this.ModelInstanceTypeToAbbreviatedModelInstanceType(modelInstanceType);
        var subModelAbbreviated = this.SubModelTypeToSubModelTypeAbbreviated(subModelType);
        return $"{IdPrefix}_cd_{abbreviatedModelInstanceType}_{modelInstanceId}_{subModelAbbreviated}_{fieldId}";
    }

    /// <inheritdoc />
    public string BuildDataSpecificationId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType, string fieldId)
    {
        var abbreviatedModelInstanceType = this.ModelInstanceTypeToAbbreviatedModelInstanceType(modelInstanceType);
        var subModelAbbreviated = this.SubModelTypeToSubModelTypeAbbreviated(subModelType);
        return $"{IdPrefix}_ds_{abbreviatedModelInstanceType}_{modelInstanceId}_{subModelAbbreviated}_{fieldId}";
    }

    /// <inheritdoc />
    public string BuildReferenceElementId(ModelInstanceType toShellModelInstance, string toModelInstanceId)
    {
        var abbreviatedModelInstanceToShell = this.ModelInstanceTypeToAbbreviatedModelInstanceType(toShellModelInstance);

        return $"{IdPrefix}_re_{abbreviatedModelInstanceToShell}_{toModelInstanceId}";
    }

    /// <inheritdoc />
    public string BuildReferenceId(string referenceId)
    {
        return $"{IdPrefix}_r_{referenceId}";
    }

    /// <inheritdoc />
    public string BuildShellId(ModelInstanceType modelInstanceType, string modelInstanceId)
    {
        var abbreviatedModelInstanceType = this.ModelInstanceTypeToAbbreviatedModelInstanceType(modelInstanceType);
        return $"{IdPrefix}_{abbreviatedModelInstanceType}_{modelInstanceId}";
    }

    /// <inheritdoc />
    public string BuildSubModelElementId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType, string fieldId)
    {
        var abbreviatedModelInstanceType = this.ModelInstanceTypeToAbbreviatedModelInstanceType(modelInstanceType);
        var subModelAbbreviated = this.SubModelTypeToSubModelTypeAbbreviated(subModelType);
        return $"{IdPrefix}_sme_{abbreviatedModelInstanceType}_{modelInstanceId}_{subModelAbbreviated}_{fieldId}";
    }

    /// <inheritdoc />
    public string BuildSubModelElementCollectionId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType)
    {
        var abbreviatedModelInstanceType = this.ModelInstanceTypeToAbbreviatedModelInstanceType(modelInstanceType);
        var subModelAbbreviated = this.SubModelTypeToSubModelTypeAbbreviated(subModelType);
        return $"{IdPrefix}_smec_{abbreviatedModelInstanceType}_{modelInstanceId}_{subModelAbbreviated}";
    }

    /// <inheritdoc />
    public string BuildSubModelElementCollectionFieldId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType, string fieldId)
    {
        var abbreviatedModelInstanceType = this.ModelInstanceTypeToAbbreviatedModelInstanceType(modelInstanceType);
        var subModelAbbreviated = this.SubModelTypeToSubModelTypeAbbreviated(subModelType);
        return $"{IdPrefix}_smec_{abbreviatedModelInstanceType}_{modelInstanceId}_{subModelAbbreviated}_{fieldId}";
    }

    /// <inheritdoc />
    public string BuildSubModelElementListId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType, string elementId, string listId)
    {
        var abbreviatedModelInstanceType = this.ModelInstanceTypeToAbbreviatedModelInstanceType(modelInstanceType);
        var subModelAbbreviated = this.SubModelTypeToSubModelTypeAbbreviated(subModelType);
        return $"{IdPrefix}_smel_{abbreviatedModelInstanceType}_{modelInstanceId}_{subModelAbbreviated}_{elementId}_{listId}";
    }

    /// <inheritdoc />
    public string BuildSubModelId(ModelInstanceType modelInstanceType, string modelInstanceId, SubModelType subModelType)
    {
        var abbreviatedModelInstanceType = this.ModelInstanceTypeToAbbreviatedModelInstanceType(modelInstanceType);
        var subModelAbbreviated = this.SubModelTypeToSubModelTypeAbbreviated(subModelType);
        return $"{IdPrefix}_sm_{abbreviatedModelInstanceType}_{modelInstanceId}_{subModelAbbreviated}";
    }

    private string SubModelTypeToSubModelTypeAbbreviated(SubModelType subModelType)
    {
        switch (subModelType)
        {
            case SubModelType.Documentation:
                return "doc";
            case SubModelType.Lines:
                return "l";
            case SubModelType.Machines:
                return "m";
            case SubModelType.Nameplate:
                return "np";
            case SubModelType.OperationalData:
                return "od";
            case SubModelType.ProcessFlow:
                return "pf";
            case SubModelType.TechnicalData:
                return "td";
            default:
                throw new Exception($"Unknown sub model type '{subModelType}'.");
        }
    }

    private string ModelInstanceTypeToAbbreviatedModelInstanceType(ModelInstanceType modelInstanceType)
    {
        switch (modelInstanceType)
        {
            case ModelInstanceType.Factory:
                return "f";
            case ModelInstanceType.Line:
                return "l";
            case ModelInstanceType.MachineType:
                return "mt";
            case ModelInstanceType.Machine:
                return "m";
            default:
                throw new Exception($"Unknown model instance type '{modelInstanceType}'.");
        }
    }
}