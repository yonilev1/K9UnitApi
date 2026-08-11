using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using K9UnitApi.Enums;
namespace K9UnitApi.Models;

//[JsonConverter(typeof(JsonStringEnumConverter))]

public class TrainingSession
{
    public int Id { get; set; }

    [Required]
    public DateTime SessionDate { get; set; }

    [Required]
    [Range(1,300)]
    public int DurationMinutes { get; set; }

    [Required]
    public TrainingType TrainingType { get; set; }

    [Required]
    [Range(0, 100)]
    public int PerformanceScore { get; set; }

    public bool Passed { get; set; }

    [Required]
    [MaxLength(100)]
    public string Evaluator { get; set; } = string.Empty;

    public Dog? Dog { get; set; }

    public int? DogId { get; set; }
}
