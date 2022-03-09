
namespace AnimalackApi.Models.Pets;

using AnimalackApi.Entities;

public class UpdatePetRequest
{
  public string Name { get; set; }
  public string Species { get; set; }
  public string Breed { get; set; }
  public string Color { get; set; }
  public PetGender Gender { get; set; }
  public DateTime DateOfBirth { get; set; }
}