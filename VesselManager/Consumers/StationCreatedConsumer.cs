using Contracts;
using MassTransit;

namespace VesselManager.Consumers;

public class StationCreatedConsumer : IConsumer<Created>
{
    public async Task Consume(ConsumeContext<Created> context)
    {
        Console.WriteLine($"Consuming Station Created: {context.Message.Name} - SeqNum: {context.Message.SequenceNumber}");
        // throw new Exception("dummy exception");
    }
}