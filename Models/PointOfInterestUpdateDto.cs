using System.ComponentModel.DataAnnotations;

namespace SimpleTutorialWebApplication.Models;

public class PointOfInterestUpdateDto
{
    [Required]
    [MaxLength(40)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }
}
