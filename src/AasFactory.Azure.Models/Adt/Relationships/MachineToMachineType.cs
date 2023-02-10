using System.Diagnostics.CodeAnalysis;
using Azure.DigitalTwins.Core;

namespace AasFactory.Azure.Models.Adt.Relationships
{
    /// <summary>
    /// The MToMT is to define relationship between the Machine and machine type twins
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MachineToMachineType : BasicRelationship
    {
        public MachineToMachineType()
        {
            this.Name = AdtConstants.DerivedFromRelationshipName;
        }
    }
}
