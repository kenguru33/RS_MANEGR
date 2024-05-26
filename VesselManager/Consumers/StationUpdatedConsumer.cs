using Contracts;
using MassTransit;

namespace VesselManager.Consumers;

public class StationUpdatedConsumer : IConsumer<Updated>
{
    public async Task Consume(ConsumeContext<Updated> context)
    {
        Console.WriteLine($"Consuming Station Updated: {context.Message.Name} - SeqNum: {context.Message.SequenceNumber}");
        // throw new Exception("dummy exception");
    }
}