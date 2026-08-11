using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.Models;

public class Handler
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    //uniqe
    public string PersonalNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string Rank { get; set; } = string.Empty;

    [Required]
    [Range(0,40)]
    public int YearsOfExperience { get; set; }

    [Required]
    [MaxLength(100)]
    public string BaseAssigned { get; set; } = string.Empty;

    public Dog? dog { get; set; }
}
