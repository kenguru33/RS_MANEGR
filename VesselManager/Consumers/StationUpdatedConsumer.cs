using Contracts;
using MassTransit;

namespace VesselManager.Consumers;

public class StationUpdatedConsumer : IConsumer<StationUpdated>
{
    public async Task Consume(ConsumeContext<StationUpdated> context)
    {
        Console.WriteLine($"Consuming Station Updated: {context.Message.Name} - SeqNum: {context.Message.SequenceNumber}");
        // throw new Exception("dummy exception");
    }
}