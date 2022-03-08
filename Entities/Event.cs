namespace AnimalackApi.Entities;

public class Event {
  public int Id { get; set; }
  public string Name { get; set; }

  // Should this be a enum?
  public string Type { get; set; }
  public List<Pet> Pets { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
}