namespace AnimalackApi.Models.Users;

public class AuthResponse
{
  public int Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Username { get; set; }
  public string JwtToken { get; set; }
}
