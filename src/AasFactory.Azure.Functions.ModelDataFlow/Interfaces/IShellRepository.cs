using AasFactory.Azure.Models.Aas.Metamodels;

namespace AasFactory.Azure.Functions.ModelDataFlow.Interfaces;

public interface IShellRepository : IBaseAdtRepository
{
    void CreateOrReplaceShell(Shell shell);
}
