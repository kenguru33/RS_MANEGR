namespace Contracts;

public class StationCreated : StationMessageBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}