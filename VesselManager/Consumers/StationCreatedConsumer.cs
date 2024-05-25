using Contracts;
using MassTransit;

namespace VesselManager.Consumers;

public class StationCreatedConsumer : IConsumer<StationCreated>
{
    public async Task Consume(ConsumeContext<StationCreated> context)
    {
        Console.WriteLine($"Consuming Station Created: {context.Message.Name} - SeqNum: {context.Message.SequenceNumber}");
        // throw new Exception("dummy exception");
    }
}