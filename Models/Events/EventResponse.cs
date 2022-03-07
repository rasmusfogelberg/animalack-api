namespace AnimalackApi.Models.Events;

using AnimalackApi.Entities;
using Newtonsoft.Json;

public class EventResponse
{
  public int Id { get; set; }
  public string Name { get; set; }

  // Should this be a enum?
  public string Type { get; set; }
  public Pet Pet { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
}