namespace AnimalackApi.Models.Users;

using System.ComponentModel.DataAnnotations;
public class AuthRequest
{
  [Required]
  [EmailAddress]
  public string Username { get; set; }

  [Required]
  public string Password { get; set; }
}