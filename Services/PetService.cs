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



    var registerPetResponse = _mapper.Map<RegisterPetResponse>(pet);

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

  public void DeleteById(int id)
  {
    var pet = getPet(id);

    _context.Pets.Remove(pet);
    _context.SaveChanges();
  }

  /* Helper methods */
  private Pet getPet(int id)
  {
    var pet = _context.Pets.Include(pet => pet.Users).Where(pet => pet.Id == id);

    var singlePetResponse = _mapper.Map<SinglePetResponse>(pet);

    /* TODO: Figure out way to populate the Users */
    List<User> users = new List<User>();
    foreach (int userId in singlePetResponse.Users)
    {
      users.AddRange(_context.Users.Where(u => u.Id == userId).ToList());
    }

    var petReturn = new Pet
    {
      Users = users,
      Name = singlePetResponse.Name,
      Species = singlePetResponse.Species,
      Breed = singlePetResponse.Breed,
      Color = singlePetResponse.Color,
      Gender = singlePetResponse.Gender,
      DateOfBirth = singlePetResponse.DateOfBirth,
    };


    if (pet == null) throw new KeyNotFoundException("Pet not found");

    return petReturn;
  }
}