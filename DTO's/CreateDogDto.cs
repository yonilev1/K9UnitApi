using K9UnitApi.Enums;
using K9UnitApi.Models;
using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.DTO_s;

public class CreateDogDto
{

    public string Name { get; set; } = string.Empty;

    public string Breed { get; set; } = string.Empty;

    public string MicrochipId { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string Specialty { get; set; } = string.Empty;

    public string? Status { get; set; }
}
