
namespace AnimalackApi.Models.Users;

using System.ComponentModel.DataAnnotations;
using AnimalackApi.Entities;

public class UpdateRequest
{
  private string _password;
  private string _confirmPassword;
  private string _username;
  public string FirstName { get; set; }
  public string LastName { get; set; }

  [EmailAddress]
  public string Username
  {
    get => _username;
    set => _username = replaceEmptyWithNull(value);
  }
  public string Password
  {
    get => _password;
    set => _password = replaceEmptyWithNull(value);
  }

  [Compare("Password")]
  public string ConfirmPassword
  {
    get => _confirmPassword;
    set => _confirmPassword = replaceEmptyWithNull(value);
  }


  private string replaceEmptyWithNull(string value)
  {
    // replace empty string with null to make field optional
    return string.IsNullOrEmpty(value) ? null : value;
  }
}