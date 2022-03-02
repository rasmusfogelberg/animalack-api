using AnimalackApi.Entities;
using AnimalackApi.Helpers;
using AnimalackApi.Models.Users;
using AutoMapper;
using BCrypt.Net;

namespace AnimalackApi.Services;

public interface IUserService
{

  // Authentication
  void Register(RegisterRequest model, string origin);
  IEnumerable<UserResponse> GetAll();
  UserResponse GetById(int id);
  UserResponse UpdateById(int id, UpdateRequest model);
  void DeleteById(int id);



  // Authorization
}

// Interface
public class UserService : IUserService
{
  private readonly DataContext _context;
  private readonly IMapper _mapper;

  // Constructor
  public UserService(DataContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  // Get all the users
  public IEnumerable<UserResponse> GetAll()
  {
    var users = _context.Users;
    return _mapper.Map<IList<UserResponse>>(users);
  }

  // Get single user by id
  public UserResponse GetById(int id)
  {
    var user = getUser(id);
    return _mapper.Map<UserResponse>(user);
  }

  // Register a user
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
    // The package is created with this silly namespace
    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

    // Save user
    _context.Users.Add(user);
    _context.SaveChanges();

    // TODO: Send verification email (if there is time)
  }

  // Update a user
  public UserResponse UpdateById(int id, UpdateRequest model)
  {
    var user = getUser(id);

    // Validates and checks if email already exists
    if (user.Username != model.Username && _context.Users.Any(user => user.Username == model.Username))
      throw new AppException($"Email '{model.Username}' is already registered");

    // Hashes the password if provided
    if (!string.IsNullOrEmpty(model.Password))
    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

    // Copy the model to the user and then save the changes
    _mapper.Map(model, user);
    _context.Users.Update(user);
    _context.SaveChanges();

    return _mapper.Map<UserResponse>(user);
  }

  // Delete a user
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