
namespace AnimalackApi.Models.Events;

using AnimalackApi.Entities;

public class UpdateEventRequest
{
  public string Name { get; set; }
  public string Type { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
}