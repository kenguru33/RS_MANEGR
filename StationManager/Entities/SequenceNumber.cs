namespace StationManager.Entities;

public class StationSequenceNumber
{
    public Guid Id { get; set; }
    public int CurrentValue { get; set; }

    public Station Station { get; set; }
}