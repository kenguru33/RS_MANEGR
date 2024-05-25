using System.ComponentModel.DataAnnotations;

namespace StationManager.Dtos;

public class CreateStationDto
{
    [Required]
    public string Name { get; set; }
}