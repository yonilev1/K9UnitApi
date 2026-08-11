using K9UnitApi.Enums;

namespace K9UnitApi.DTO_s;

public class GetDogByIdDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Breed { get; set; } = string.Empty;

    public string MicrochipId { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public String Specialty { get; set; } = string.Empty;

    public String Status { get; set; } = string.Empty;
}
