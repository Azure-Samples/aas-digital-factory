namespace AasFactory.Azure.Models.Adt
{
    public static class AdtConstants
    {
        // Model Ids
        public const string AASModelId = "dtmi:digitaltwins:aas:AssetAdministrationShell;1";
        public const string AssetInfoModelId = "dtmi:digitaltwins:aas:AssetInformation;1";
        public const string PropertyModelId = "dtmi:digitaltwins:aas:Property;1";
        public const string BooleanPropertyModelId = "dtmi:digitaltwins:aas:ext:BooleanProperty;1";
        public const string DatePropertyModelId = "dtmi:digitaltwins:aas:ext:DateProperty;1";
        public const string DateTimePropertyModelId = "dtmi:digitaltwins:aas:ext:DateTimeProperty;1";
        public const string DurationPropertyModelId = "dtmi:digitaltwins:aas:ext:DurationProperty;1";
        public const string FloatPropertyModelId = "dtmi:digitaltwins:aas:ext:FloatProperty;1";
        public const string IntegerPropertyModelId = "dtmi:digitaltwins:aas:ext:IntegerProperty;1";
        public const string LongPropertyModelId = "dtmi:digitaltwins:aas:ext:LongProperty;1";
        public const string StringPropertyModelId = "dtmi:digitaltwins:aas:ext:StringProperty;1";
        public const string TimePropertyModelId = "dtmi:digitaltwins:aas:ext:TimeProperty;1";
        public const string DoublePropertyModelId = "dtmi:digitaltwins:aas:ext:DoubleProperty;1";
        public const string ReferenceModelId = "dtmi:digitaltwins:aas:Reference;1";
        public const string ReferenceElementModelId = "dtmi:digitaltwins:aas:ReferenceElement;1";
        public const string SubmodelModelId = "dtmi:digitaltwins:aas:Submodel;1";
        public const string SubmodelElementListModelId = "dtmi:digitaltwins:aas:SubmodelElementList;1";
        public const string SubmodelElementCollectionModelId = "dtmi:digitaltwins:aas:SubmodelElementCollection;1";
        public const string ConceptDescriptionModelId = "dtmi:digitaltwins:aas:ConceptDescription;1";
        public const string DataSpecificationModelId = "dtmi:digitaltwins:aas:DataSpecificationIEC61360;1";

        // Relationship names
        public const string SubmodelRelationshipName = "submodel";
        public const string DerivedFromRelationshipName = "derivedFrom";
        public const string ReferredElementRelationshipName = "referredElement";
        public const string SubmodelElementRelationshipName = "submodelElement";
        public const string ValueRelationshipName = "value";
        public const string SemanticIdRelationshipName = "semanticId";
        public const string DataSpecificationRelationshipName = "dataSpecification";

        // Property Value Keys
        public const string BooleanPropertyKey = "booleanValue";
        public const string DatePropertyKey = "dateValue";
        public const string DateTimePropertyKey = "dateTimeValue";
        public const string DoublePropertyKey = "doubleValue";
        public const string DurationPropertyKey = "durationValue";
        public const string FloatPropertyKey = "floatValue";
        public const string IntPropertyKey = "intValue";
        public const string LongPropertyKey = "longValue";
        public const string StringPropertyKey = "value";
        public const string TimePropertyKey = "timeValue";
    }
}