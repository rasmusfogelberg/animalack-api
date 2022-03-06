
namespace AnimalackApi.Models.Pets;

using System.ComponentModel.DataAnnotations;
using AnimalackApi.Entities;

public class UpdatePetRequest
{
  public User User { get; set; }
  public string Name { get; set; }
  public string Specie { get; set; }
  public string Breed { get; set; }
  public string Color { get; set; }
  public enum Gender { Male, Female }
  public DateTime DateOfBirth { get; set; }
}