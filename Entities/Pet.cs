namespace AnimalackApi.Entities;

public class Pet
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Specie { get; set; }
  public string Breed { get; set; }
  public string Color { get; set; }
  public enum Gender { Male, Female}
  public DateTime DateOfBirth { get; set; }
  public ICollection<User> Users { get; set; }
}