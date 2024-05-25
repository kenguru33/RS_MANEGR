namespace Contracts;

public class StationUpdated : StationMessageBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
}