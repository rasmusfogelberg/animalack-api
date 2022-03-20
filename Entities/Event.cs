namespace AnimalackApi.Entities;

public class Event {
  public int Id { get; set; }
  public string Name { get; set; }
  public string Type { get; set; }
  public Pet Pet { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
}