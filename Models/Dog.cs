using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using K9UnitApi.Enums;
namespace K9UnitApi.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public class Dog
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Breed { get; set; } = string.Empty;

    [Required]
    [MaxLength(15)]
    //uniqe
    public string MicrochipId { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public Specialty Specialty { get; set; }


    [Required]
    public Status Status { get; set; }

    public Handler? Handler { get; set; }

    public int? HandlerId { get; set; }

    public List<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
}
