namespace AnimalackApi.Models.Events;

using System.Text.Json.Serialization;
using AnimalackApi.Entities;
using AnimalackApi.Models.Pets;

public class SingleEventResponse
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Type { get; set; }
  public PetResponse Pet { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
}