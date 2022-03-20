using AnimalackApi.Entities;
using AnimalackApi.Helpers;
using AnimalackApi.Models.Pets;
using AutoMapper;
using AnimalackApi.Helpers.JWT;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace AnimalackApi.Services;

public interface IPetService
{
  void RegisterPet(RegisterPetRequest model, string origin);
  IEnumerable<PetResponse> GetAll();
  SinglePetResponse GetById(int id);
  SinglePetResponse UpdateById(int id, UpdatePetRequest model);
  void DeleteById(int id);
}

// Class
public class PetService : IPetService
{
  private readonly DataContext _context;
  private readonly IMapper _mapper;

  // Constructor
  public PetService(DataContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  // Get all Pets
  public IEnumerable<PetResponse> GetAll()
  {
    var pets = _context.Pets.Include(pet => pet.Users);
    return _mapper.Map<IList<PetResponse>>(pets);
  }

  // Get a single Pet by id
  public SinglePetResponse GetById(int id)
  {
    var pet = getPet(id);
    return _mapper.Map<SinglePetResponse>(pet);
  }

  // Register a Pet
  public void RegisterPet(RegisterPetRequest model, string origin)
  {
    List<User> users = new List<User>();

    foreach (int userId in model.Users)
    {
      users.AddRange(_context.Users.Where(u => u.Id == userId).ToList());
    }

    if (users == null || users.Count == 0) throw new KeyNotFoundException("No users found matching the passed ids.");

    var pet = new Pet
    {
      Users = users,
      Name = model.Name,
      Species = model.Species,
      Breed = model.Breed,
      Color = model.Color,
      Gender = model.Gender,
      DateOfBirth = model.DateOfBirth,
    };

    _context.Pets.Add(pet);
    _context.SaveChanges();
  }

  public SinglePetResponse UpdateById(int id, UpdatePetRequest model)
  {
    var pet = getPet(id);

    _mapper.Map(model, pet);
    _context.Pets.Update(pet);
    _context.SaveChanges();

    return _mapper.Map<SinglePetResponse>(pet);
  }

  // Delete a Pet
  public void DeleteById(int id)
  {
    var pet = getPet(id);

    _context.Pets.Remove(pet);
    _context.SaveChanges();
  }

  /* Helper methods */

  // Get a single Pet by id
  private Pet getPet(int id)
  {
    var pet = _context.Pets.Find(id);
    _context.Pets.Include(pet => pet.Users).ToList();

    if (pet == null) throw new KeyNotFoundException("Pet not found");

    return pet;
  }
}