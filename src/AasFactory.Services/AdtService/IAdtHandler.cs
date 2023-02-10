namespace AasFactory.Services;

public interface IAdtHandler
{
    public IAdtClient GetAdtClient(string instanceUrl, bool continueWithErrors);
}
