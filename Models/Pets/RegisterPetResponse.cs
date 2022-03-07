namespace AnimalackApi.Models.Pets;

using AnimalackApi.Entities;

public class RegisterPetResponse
{
  public int Id { get; set; }
  public List<User> Users { get; set; }
  public string Name { get; set; }
  public string Species { get; set; }
  public string Breed { get; set; }
  public string Color { get; set; }
  public PetGender Gender { get; set; }
  public DateTime DateOfBirth { get; set; }
}