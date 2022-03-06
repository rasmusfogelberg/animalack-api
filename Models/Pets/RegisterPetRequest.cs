namespace AnimalackApi.Models.Pets;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AnimalackApi.Entities;

public class RegisterPetRequest
{
  public int Id { get; set; }
  [Required]
  [Column("Owner")]
  public User User { get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  public string Specie { get; set; }
  public string Breed { get; set; }
  [Required]
  public string Color { get; set; }
  public enum Gender { Male, Female }
  public DateTime DateOfBirth { get; set; }
}