
using AnimalackApi.Entities;
using AnimalackApi.Models.Pets;

namespace AnimalackApi.Models.Users;

public class SingleUserResponse
{
  public int Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Username { get; set; }
  public List<PetResponse> Pets { get; set; }

  // TODO: Add Role, CreatedAt, UpdatedAt
}