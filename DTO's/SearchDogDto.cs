using K9UnitApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.DTO_s;

public class SearchDogDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Breed { get; set; } = string.Empty;

    public string Specialty { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
