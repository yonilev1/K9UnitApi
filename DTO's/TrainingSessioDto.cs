using K9UnitApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.DTO_s;

public class TrainingSessioDto
{
    public int DogId { get; set; }

    public DateTime SessionDate { get; set; }

    public int DurationMinutes { get; set; }

    public string TrainingType { get; set; } = string.Empty;

    public int PerformanceScore { get; set; }

    public string Evaluator { get; set; } = string.Empty;
}
