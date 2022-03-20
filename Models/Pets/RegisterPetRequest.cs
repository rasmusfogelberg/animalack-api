namespace AnimalackApi.Models.Pets;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AnimalackApi.Entities;

public class RegisterPetRequest
{
  public int Id { get; set; }
  [Required]
  public List<int> Users { get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  public string Species { get; set; }
  public string Breed { get; set; }
  [Required]
  public string Color { get; set; }
  public PetGender Gender { get; set; }
  public DateTime DateOfBirth { get; set; }
}