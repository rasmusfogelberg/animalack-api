namespace AnimalackApi.Models.Events;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AnimalackApi.Entities;

public class AddEventRequest
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  public string Type { get; set; }
  [Required]
  public int Pet { get; set; }
  [Required]
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
}