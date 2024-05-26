namespace VesselManager.Entities;

public class ConsumerSequenceNumber
{
    public string ConsumerName { get; set; }  // The name of the consumer (e.g., "StationService")
    public Guid Id { get; set; }      // The unique identifier for the entity instance
    public int LastProcessedSequenceNumber { get; set; }  // The last processed sequence number for this entity
}

