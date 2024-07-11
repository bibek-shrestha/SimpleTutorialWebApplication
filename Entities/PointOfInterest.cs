﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleTutorialWebApplication.Entities;

namespace SimpleTutorialWebApplication;

public class PointOfInterest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; };

    [ForeignKey("CityId")]
    public City? city { get; set; }

    public int CityId { get; set; }

    public PointOfInterest(string name)
    {
        Name = name;
    }

}