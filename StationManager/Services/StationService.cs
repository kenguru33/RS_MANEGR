using Contracts;
using MassTransit;
using StationManager.Data;
using StationManager.Dtos;
using StationManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace StationManager.Services;

public class StationService : IStationService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly StationDbContext _stationDbContext;

    public StationService(IPublishEndpoint publishEndpoint, StationDbContext stationDbContext)
    {
        _publishEndpoint = publishEndpoint;
        _stationDbContext = stationDbContext;
    }

    public async Task<StationResponseDto> CreateStation(CreateStationDto createStationDto)
    {
        var station = new Station()
        {
            Id = new Guid(),
            Name = createStationDto.Name
        };
        // we are using outbox pattern and this is bounded to the db context
        // On db write failure It will not be put on outbox nor put on event bus.
        await _publishEndpoint.Publish(new Created()
        {
            Id = station.Id,
            Name = station.Name,
            SequenceNumber = await GetStationSequenceNumber(station)
        });
        _stationDbContext.Add(station);
        var result = await _stationDbContext.SaveChangesAsync() > 0;
        if (!result) throw new Exception("Failed to save station to database");
        return new StationResponseDto()
        {
            Id = station.Id,
            Name = station.Name
        };
    }
    
    private async Task<int> GetStationSequenceNumber(Station station)
    {
        var nextStationSequenceNumber = await _stationDbContext.SequenceNumber.FirstOrDefaultAsync(s => s.Station.Id == station.Id);
        if (nextStationSequenceNumber is not null)
        {
            nextStationSequenceNumber.CurrentValue = nextStationSequenceNumber.CurrentValue + 1;
            _stationDbContext.SequenceNumber.Update(nextStationSequenceNumber);
        }
        else
        {
            nextStationSequenceNumber = new StationSequenceNumber()
            {
                CurrentValue = 0,
                Station = station
            };
            _stationDbContext.Add(nextStationSequenceNumber);
        }
        
        return nextStationSequenceNumber.CurrentValue;
    }
    
}