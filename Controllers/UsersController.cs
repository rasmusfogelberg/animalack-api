namespace AnimalackApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AnimalackApi.Entities;
using AnmialackApi.Helpers;
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

  // GET: api/User
  [HttpGet]
  public ActionResult<IEnumerable<User>> GetUsers()
  {
    var users = _userService.GetAll();
    return Ok(users);
  }

  // GET: api/User/5
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
  /* 
    // PUT: api/User/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
      if (id != user.Id)
      {
        return BadRequest();
      }

      _context.Entry(user).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }*/

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

