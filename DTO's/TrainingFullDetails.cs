using K9UnitApi.Enums;
using K9UnitApi.Models;
using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.DTO_s;

public class TrainingFullDetails
{
    public int Id { get; set; }

    public DateTime SessionDate { get; set; }

    public int DurationMinutes { get; set; }

    public string TrainingType { get; set; } = string.Empty;

    public int PerformanceScore { get; set; }

    public bool Passed { get; set; }

    public string Evaluator { get; set; } = string.Empty;

    public string DogName { get; set; } = string.Empty;

    public string DogSpetiality { get; set; } = string.Empty;

    public string? HandlerName { get; set; } = string.Empty;

}
