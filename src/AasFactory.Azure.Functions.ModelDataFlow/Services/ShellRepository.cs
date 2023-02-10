using AasFactory.Azure.Functions.ModelDataFlow.Interfaces;
using AasFactory.Azure.Models.Aas.Metamodels;
using AasFactory.Services;
using Azure.DigitalTwins.Core;
using Microsoft.Extensions.Logging;
using Aas = AasFactory.Azure.Models.Aas.Metamodels;
using Adt = AasFactory.Azure.Models.Adt;

namespace AasFactory.Azure.Functions.ModelDataFlow.Services;

public class ShellRepository : BaseAdtRepository, IShellRepository
{
    private IPropertyRepository propertyRepository;

    public ShellRepository(
        ILogger<ShellRepository> logger,
        IAdtHandler adtSdkHandler,
        IAdtTracker adtTracker,
        IPropertyRepository propertyRepository,
        IModelDataFlowSettings settings,
        IAdtRelationshipIdBuilder idBuilder)
            : base(adtSdkHandler, settings, logger, adtTracker, idBuilder)
    {
        this.propertyRepository = propertyRepository;
    }

    public void CreateOrReplaceShell(Shell aas)
    {
        this.CreateOrReplaceTwin(new Adt.Twins.AAS(aas));

        if (aas.ReferenceElementIds.Count() > 0)
        {
            foreach (var refElementId in aas.ReferenceElementIds)
            {
                this.CreateOrReplaceRelationship<Adt.Relationships.SubModelToAAS>(refElementId, aas.Id);
            }
        }

        if (!string.IsNullOrWhiteSpace(aas.DerivedFrom))
        {
            this.CreateOrReplaceRelationship<Adt.Relationships.MachineToMachineType>(aas.Id, aas.DerivedFrom.Trim());
        }

        this.CreateSubmodelGraphTwins(aas.SubModels);

        this.CreateSubmodelGraphRelationships(aas.Id, aas.SubModels);
    }

    /// <summary>
    /// This method creates the necessary twins for the submodelElementCollection sub graph
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    private void CreateSubmodelElementCollectionTwins(Aas.SubModelElementCollection collection)
    {
        this.CreateOrReplaceTwin(new Adt.Twins.SubModelElementCollection(collection));

        foreach (var nestedCollection in collection.SubModelElementCollections)
        {
            this.CreateSubmodelElementCollectionTwins(nestedCollection);
        }

        foreach (var elementList in collection.SubModelElementLists)
        {
            this.CreateSubmodelElementListTwins(elementList);
        }

        this.CreateSubModelElementListReferenceElementsTwins(collection.ReferenceElements);
    }

    /// <summary>
    /// This method creates the necessary twins for the submodelElementList sub graph
    /// </summary>
    /// <param name="elementList"></param>
    /// <returns></returns>
    private void CreateSubmodelElementListTwins(Aas.SubModelElementList elementList)
    {
        this.CreateOrReplaceTwin(new Adt.Twins.SubModelElementList(elementList));

        this.CreateSubModelElementListReferenceElementsTwins(elementList.ReferenceElements);

        this.CreateSubModelElementListPropertiesTwins(elementList.Properties);
    }

    /// <summary>
    /// This method creates the necessary twins for the submodel sub graph
    /// </summary>
    /// <param name="submodels"></param>
    /// <returns></returns>
    private void CreateSubmodelGraphTwins(IEnumerable<Aas.SubModel> submodels)
    {
        foreach (var submodel in submodels)
        {
            this.CreateOrReplaceTwin(new Adt.Twins.Submodel(submodel));

            foreach (var referenceElement in submodel.ReferenceElements)
            {
                this.CreateOrReplaceTwin(new Adt.Twins.ReferenceElement(referenceElement));
            }

            foreach (var property in submodel.Properties)
            {
                this.propertyRepository.CreateOrReplaceProperty(property);
            }

            foreach (var element in submodel.SubModelElementLists)
            {
                this.CreateSubmodelElementListTwins(element);
            }

            foreach (var collection in submodel.SubModelElementCollections)
            {
                this.CreateSubmodelElementCollectionTwins(collection);
            }
        }
    }

    /// <summary>
    /// This method creates the necessary relationships for the submodel sub graph
    /// </summary>
    /// <param name="aasId"></param>
    /// <param name="submodels"></param>
    /// <returns></returns>
    private void CreateSubmodelGraphRelationships(string aasId, IEnumerable<Aas.SubModel> submodels)
    {
        foreach (var submodel in submodels)
        {
            this.CreateOrReplaceRelationship<Adt.Relationships.AASToSubmodel>(aasId, submodel.Id);

            foreach (var referenceElement in submodel.ReferenceElements)
            {
                this.CreateOrReplaceRelationship<Adt.Relationships.SubModelToProperty>(submodel.Id, referenceElement.Id);
            }

            foreach (var property in submodel.Properties)
            {
                this.CreateOrReplaceRelationship<Adt.Relationships.SubModelToProperty>(submodel.Id, property.Id);

                if (property.SemanticIdReference is not null)
                {
                    var referenceTwin = new Adt.Twins.Reference(property.SemanticIdReference);
                    this.CreateOrReplaceTwin<Adt.Twins.Reference>(referenceTwin);
                    this.CreateOrReplaceRelationship<Adt.Relationships.PropertyToReference>(property.Id, property.SemanticIdReference.Id);
                }
            }

            foreach (var element in submodel.SubModelElementLists)
            {
                this.CreateOrReplaceRelationship<Adt.Relationships.SubModelToProperty>(submodel.Id, element.Id);

                this.CreateSubModelElementListPropertyRelationships(element.Id, element.Properties);

                this.CreateSubModelElementListReferenceElementsRelationships(element.Id, element.ReferenceElements);
            }

            foreach (var collection in submodel.SubModelElementCollections)
            {
                this.CreateSubModelElementCollectionRelationships<Adt.Relationships.SubModelToElementCollection>(submodel.Id, collection);
            }
        }
    }

    /// <summary>
    /// Creating sub model elements list reference element twins for sub models
    /// </summary>
    /// <param name="subModels"></param>
    /// <returns></returns>
    private void CreateSubModelElementListReferenceElementsTwins(IEnumerable<Aas.ReferenceElement> refElements)
    {
        foreach (var refElement in refElements)
        {
            this.CreateOrReplaceTwin(new Adt.Twins.ReferenceElement(refElement));
        }
    }

    /// <summary>
    /// Creating sub model elements list reference element twins for sub models
    /// </summary>
    /// <param name="subModels"></param>
    /// <returns></returns>
    private void CreateSubModelElementListPropertiesTwins(IEnumerable<Aas.Property> properties)
    {
        foreach (var property in properties)
        {
            this.propertyRepository.CreateOrReplaceProperty(property);
        }
    }

    /// <summary>
    /// sub model element list and property relationship
    /// </summary>
    /// <param name="subModelElementListId"></param>
    /// <param name="refElements"></param>
    /// <returns></returns>
    private void CreateSubModelElementListPropertyRelationships(string subModelElementListId, IEnumerable<Aas.Property> properties)
    {
        foreach (var prop in properties)
        {
            this.CreateOrReplaceRelationship<Adt.Relationships.SubModelElementListToAas>(subModelElementListId, prop.Id);
        }
    }

    /// <summary>
    /// sub model element list and property relationship
    /// </summary>
    /// <param name="subModelId"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    private void CreateSubModelElementListRelationships(string subModelId, Aas.SubModelElementList element)
    {
        this.CreateOrReplaceRelationship<Adt.Relationships.SubModelElementCollectionToElementList>(subModelId, element.Id);

        this.CreateSubModelElementListPropertyRelationships(element.Id, element.Properties);

        this.CreateSubModelElementListReferenceElementsRelationships(element.Id, element.ReferenceElements);
    }

    /// <summary>
    /// sub model element collection relationship
    /// </summary>
    /// <typeparamref name="TRelationship">The type of ADT relationship between the source node and the resulting Element Collection Node.</typeparamref>
    /// <param name="subModelId"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    private void CreateSubModelElementCollectionRelationships<TRelationship>(string subModelId, Aas.SubModelElementCollection collection)
        where TRelationship : BasicRelationship, new()
    {
        this.CreateOrReplaceRelationship<TRelationship>(subModelId, collection.Id);

        foreach (var nestedCollection in collection.SubModelElementCollections)
        {
            this.CreateSubModelElementCollectionRelationships<Adt.Relationships.SubModelElementCollectionToElementCollection>(collection.Id, nestedCollection);
        }

        foreach (var element in collection.SubModelElementLists)
        {
            this.CreateSubModelElementListRelationships(collection.Id, element);
        }

        this.CreateSubModelElementListReferenceElementsRelationships(collection.Id, collection.ReferenceElements);
    }

    /// <summary>
    /// Create sub model element list and reference element relationship
    /// </summary>
    /// <param name="subModelElementListId"></param>
    /// <param name="refElements"></param>
    /// <returns></returns>
    private void CreateSubModelElementListReferenceElementsRelationships(string subModelElementListId, IEnumerable<Aas.ReferenceElement> refElements)
    {
        foreach (var element in refElements)
        {
            this.CreateOrReplaceRelationship<Adt.Relationships.SubModelElementListToAas>(subModelElementListId, element.Id);
        }
    }
}