using StationManager.Dtos;

namespace StationManager.Services;

public interface IStationService
{
    Task<StationResponseDto> CreateStation(CreateStationDto createStationDto);
}