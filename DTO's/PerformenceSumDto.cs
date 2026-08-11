namespace K9UnitApi.DTO_s;

public class PerformenceSumDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Specialty { get; set; } = string.Empty;

    public int NumberOfTrainings { get; set; } 

    public double? AveragePerformence { get; set; }
}
