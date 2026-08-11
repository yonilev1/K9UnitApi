namespace K9UnitApi.DTO_s;

public class DogsWithHandlerDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Breed { get; set; } = string.Empty;

    public string Specialty { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string? HandlerName { get; set; } = string.Empty;

    public string? HandlerNRank { get; set; } = string.Empty;
}
