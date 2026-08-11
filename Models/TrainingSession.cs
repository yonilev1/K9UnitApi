using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.Models;

public class TrainingSession
{
    public int Id { get; set; }

    [Required]
    public DateTime SessionDate { get; set; }

    [Required]
    [Range(1,300)]
    public int DurationMinutes { get; set; }
}
