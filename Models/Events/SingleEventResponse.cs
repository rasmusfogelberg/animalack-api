namespace AnimalackApi.Models.Events;

using System.Text.Json.Serialization;
using AnimalackApi.Entities;

public class SingleEventResponse
{
  public int Id { get; set; }
  public string Name { get; set; }

  // Should this be a enum?
  public string Type { get; set; }
  // This should be a single pet. But it is...?
  public Pet Pet { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
}