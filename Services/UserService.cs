using AnimalackApi.Entities;
using AnimalackApi.Helpers;
using AnimalackApi.Helpers.JWT;
using AnimalackApi.Models.Users;
using AutoMapper;
using BCrypt.Net;

namespace AnimalackApi.Services;

// Interface
public interface IUserService
{

  // Authentication
  AuthResponse Authenticate(AuthRequest model);
  void Register(RegisterRequest model, string origin);
  IEnumerable<UserResponse> GetAll();
  UserResponse GetById(int id);
  UserResponse UpdateById(int id, UpdateRequest model);
  void DeleteById(int id);
}

// Class
public class UserService : IUserService
{
  private readonly DataContext _context;
  private readonly IMapper _mapper;
  private readonly IJWTUtils _jwtUtils;

  // Constructor
  public UserService(DataContext context, IMapper mapper, IJWTUtils jwtUtils)
  {
    _context = context;
    _mapper = mapper;
    _jwtUtils = jwtUtils;
  }

  // Authenticate credentials
  public AuthResponse Authenticate(AuthRequest model)
  {
    var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);

    if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
    {
      throw new AppException("Username or Password is incorrect");
    }

    var jwtToken = _jwtUtils.GenerateToken(user);

    var response = _mapper.Map<AuthResponse>(user);
    response.JwtToken = jwtToken;

    return response;
  }

  // Get all the Users
  public IEnumerable<UserResponse> GetAll()
  {
    var users = _context.Users;
    return _mapper.Map<IList<UserResponse>>(users);
  }

  // Get single User by id
  public UserResponse GetById(int id)
  {
    var user = getUser(id);
    return _mapper.Map<UserResponse>(user);
  }

  // Register a User
  public void Register(RegisterRequest model, string origin)
  {
    // Validate if a User with this email already exists
    if (_context.Users.Any(user => user.Username == model.Username))
    {
      return;
    }

    // Use model to create a new User object
    var user = _mapper.Map<User>(model);

    // Hash password
    // The package is created with this silly namespace
    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

    // Save User
    _context.Users.Add(user);
    _context.SaveChanges();

    // TODO: Send verification email (if there is time)
  }

  // Update a User
  public UserResponse UpdateById(int id, UpdateRequest model)
  {
    var user = getUser(id);

    // Hashes the password if provided
    if (!string.IsNullOrEmpty(model.Password))
      user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

    // Copy the model to the User and then save the changes
    _mapper.Map(model, user);
    _context.Users.Update(user);
    _context.SaveChanges();

    return _mapper.Map<UserResponse>(user);
  }

  // Delete a User
  public void DeleteById(int id)
  {
    var user = getUser(id);

    _context.Users.Remove(user);
    _context.SaveChanges();

  }

  /* Helper methods */
  private User getUser(int id)
  {
    var user = _context.Users.Find(id);

    if (user == null) throw new KeyNotFoundException("User not found");

    return user;
  }



}