using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StationManager.Data;
using StationManager.Dtos;
using StationManager.Entities;

namespace StationManager.Controllers;

[ApiController]
[Route("[controller]")]
public class StationController : Controller
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly StationDbContext _stationDbContext;

    public StationController(IPublishEndpoint publishEndpoint, StationDbContext stationDbContext)
    {
        _publishEndpoint = publishEndpoint;
        _stationDbContext = stationDbContext;
    }
    // GET
    [HttpGet(Name = "GetStations")]
    public async Task<IActionResult> Index()
    {
        var stations = await _stationDbContext.Stations.ToListAsync();
        return Ok(stations);
    }

    // POST
    [HttpPost(Name = "CreateStation")]
    public async Task<IActionResult> CreateStation([FromBody] CreateStationDto createStationDto)
    {
        var station = new Station()
        {
            Id = new Guid(),
            Name = createStationDto.Name
        };
        await _publishEndpoint.Publish(new StationCreated()
        {
            Id = station.Id,
            Name = station.Name,
            SequenceNumber = await GetStationSequenceNumber(station)
        });
        _stationDbContext.Add(station);
        var result = await _stationDbContext.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Could not save to database!");
        return Ok(station);
    }

    [HttpPut("api/[action]/{id}")]
    public async Task<IActionResult> UpdateStation(Guid id, [FromBody] UpdateStationDto updateStationDto)
    {
        var station = await _stationDbContext.Stations.FirstOrDefaultAsync(s => s.Id == id);
        if (station is null)
        {
            return NotFound();
        }

        station.Name = updateStationDto.Name;
        
        await _publishEndpoint.Publish<StationUpdated>(new StationCreated()
        {
            Id = station.Id,
            Name = station.Name,
            SequenceNumber = await GetStationSequenceNumber(station)
        });
        
        _stationDbContext.Stations.Update(station);
        var result = await _stationDbContext.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Could not save to database!");
        return Ok(station);
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