namespace AnimalackApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AnimalackApi.Entities;
using AnimalackApi.Helpers;
using AnimalackApi.Services;
using AnimalackApi.Models.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
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

  // Get all users
  [HttpGet]
  public ActionResult<IEnumerable<User>> GetUsers()
  {
    var users = _userService.GetAll();
    return Ok(users);
  }

  // Get a single user
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

  // Edit a user
  [HttpPut("{id}")]
  public ActionResult<UserResponse> UpdateUser(int id, UpdateRequest model)
  {
    // Find out how I get user.Id
    // if (id != User.Id) return Unauthorized(new { message = "Unauthorized"});


    var user = _userService.UpdateById(id, model);

    return Ok(new { message = "User successfully updated!" });
  }

  // Register a user
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
    // Used to have `var user` infront of this line. Did not work with void
    _userService.DeleteById(id);

    // How can I do a check if there is a user on the specific id? Or do I even need it 
    // since there is a check in the "getUser" and "DeleteById" methods in UserService class?
    /*    if (user == null)
       {
         return NotFound();
       } */

    return Ok(new { message = "User successfully deleted!" });
  }


  /*
    private bool UserExists(int id)
    {
      return _context.Users.Any(e => e.Id == id);
    } */
}

