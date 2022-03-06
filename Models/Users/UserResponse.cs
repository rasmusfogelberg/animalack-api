using AnimalackApi.Entities;

namespace AnimalackApi.Models.Users;

public class UserResponse
{
  public int Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Username { get; set; }
  public List<Pet> Pets { get; set; }

  // TODO: Add Role, CreatedAt, UpdatedAt
}