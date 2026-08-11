namespace K9UnitApi.DTO_s;

public class CreatedTraininSessionDto
{
    public int DogId { get; set; }

    public DateTime SessionDate { get; set; }

    public int DurationMinutes { get; set; }

    public string TrainingType { get; set; } = string.Empty;

    public int PerformanceScore { get; set; }

    public bool Passed { get; set; }

    public string Evaluator { get; set; } = string.Empty;
}
