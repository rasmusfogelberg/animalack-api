namespace AnimalackApi.Models.Events;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AnimalackApi.Entities;

public class AddEventRequest
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }

  // Should this be a enum?
  public string Type { get; set; }
  [Required]
  public Pet Pet { get; set; }
  [Required]
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
}