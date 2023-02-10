namespace AasFactory.Azure.Models.EventHubs.Events.V1;

public class FactoryModelDataEventChanged : ModelDataEventChanged
{
    /// <summary>
    /// Gets or sets the output file name.
    /// </summary>
    public string OutputFileName { get; set; } = string.Empty;
}
