namespace VesselManager.Entities;

public class PublisherSequenceNumber
{
    public string PublisherName { get; set; }  // The name of the publisher (e.g., "StationPublisher", "VehiclePublisher")
    public Guid Id { get; set; }       // The unique identifier for the entity instance
    public int CurrentSequenceNumber { get; set; }  // The current sequence number for this entity
}
