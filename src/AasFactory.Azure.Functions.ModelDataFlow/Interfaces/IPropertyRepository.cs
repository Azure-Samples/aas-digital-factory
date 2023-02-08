using Aas = AasFactory.Azure.Models.Aas.Metamodels;

namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces
{
    public interface IPropertyRepository : IBaseAdtRepository
    {
        void CreateOrReplaceProperty(Aas.Property property);
    }
}