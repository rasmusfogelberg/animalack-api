using System.ComponentModel.DataAnnotations;

namespace AnimalackApi.Models.Users;

public class RegisterRequest
{
  [Required]
  public string FirstName { get; set; }
  [Required]
  public string LastName { get; set; }
  [Required]
  public string Username { get; set; }
  [Required]
  public string Password { get; set; }
  [Required]
  [Compare("Password")]
  public string ConfirmPassword { get; set; }
}