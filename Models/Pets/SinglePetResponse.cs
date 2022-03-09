namespace AnimalackApi.Models.Pets;


using AnimalackApi.Entities;
using AnimalackApi.Models.Users;
using Newtonsoft.Json;

public class SinglePetResponse
{
  public int Id { get; set; }
  /* [JsonIgnore] */
  public List<UserResponse> Users { get; set; }
  public string Name { get; set; }
  public string Species { get; set; }
  public string Breed { get; set; }
  public string Color { get; set; }
  public PetGender Gender { get; set; }
  public DateTime DateOfBirth { get; set; }
}