using AnimalackApi.Entities;
using AnimalackApi.Models.Users;
using AnmialackApi.Helpers;
using AutoMapper;
using BCrypt.Net;

namespace AnimalackApi.Services;

public interface IUserService
{

  // Authentication
  void Register(RegisterRequest model, string origin);
  IEnumerable<UserResponse> GetAll();
  UserResponse GetById(int id);

  // Authorization
}

public class UserService : IUserService
{
  private readonly DataContext _context;
  private readonly IMapper _mapper;


  public UserService(DataContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public IEnumerable<UserResponse> GetAll()
  {
    var users = _context.Users;
    return _mapper.Map<IList<UserResponse>>(users);
  }

  public UserResponse GetById(int id)
  {
    var user = getUser(id);
    return _mapper.Map<UserResponse>(user);
  }

  public void Register(RegisterRequest model, string origin)
  {
    // Validate if a user with this email already exists
    if (_context.Users.Any(user => user.Username == model.Username))
    {
      return;
    }

    // Use model to create a new user object
    var user = _mapper.Map<User>(model);

    // Hash password
    // Not my idea. The package is created with this silly namespace
    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

    // Save user
    _context.Users.Add(user);
    _context.SaveChanges();

    // Send verification email (if there is time)
  }

  /* Helper methods */
  private User getUser(int id)
  {
    var user = _context.Users.Find(id);

    if (user == null)
    throw new KeyNotFoundException("User not found");

    return user;
  }
}