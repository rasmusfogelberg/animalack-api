namespace AnimalackApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AnimalackApi.Entities;
using AnimalackApi.Helpers;
using AnimalackApi.Services;
using AnimalackApi.Models.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : AbstractController
{
  private readonly IUserService _userService;

  public UsersController(IUserService userService)
  {
    _userService = userService;
  }

  // Authenticate request Post
  [HttpPost("auth")]
  public ActionResult<AuthResponse> Auth(AuthRequest model)
  {
    var response = _userService.Authenticate(model);

    return Ok(response);
  }

  // Get all Users
  [HttpGet]
  public ActionResult<IEnumerable<User>> GetUsers()
  {
    var users = _userService.GetAll();
    return Ok(users);
  }

  // Get a single User
  [HttpGet("{id}")]
  public ActionResult<User> GetUser(int id)
  {
    var user = _userService.GetById(id);

    if (user == null)
    {
      return NotFound();
    }

    return Ok(user);
  }

  // Edit a User
  [HttpPut("{id}")]
  public ActionResult<UserResponse> UpdateUser(int id, UpdateRequest model)
  {
      
    if (AuthenticatedUser == null || id != AuthenticatedUser.Id) return Unauthorized(new { message = "Unauthorized"});

    var user = _userService.UpdateById(id, model);

    return Ok(new { message = "User successfully updated!" });
  }

  // Register a User
  [HttpPost("register")]
  public IActionResult Register(RegisterRequest model)
  {
    _userService.Register(model, Request.Headers["origin"]);

    return Ok(new { message = "User successfully registered!" });
  }

  // Delete a specific User
  [HttpDelete("{id}")]
  public IActionResult DeleteUser(int id)
  {
    _userService.DeleteById(id);

    return Ok(new { message = "User successfully deleted!" });
  }
}

