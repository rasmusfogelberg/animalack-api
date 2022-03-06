using AnimalackApi.Entities;
using AnimalackApi.Helpers;
using AnimalackApi.Models.Pets;
using AutoMapper;
using AnimalackApi.Helpers.JWT;
using BCrypt.Net;

namespace AnimalackApi.Services;

public interface IPetService
{
  void RegisterPet(RegisterPetRequest model, string origin);
  IEnumerable<PetResponse> GetAll();
  PetResponse GetById(int id);
  PetResponse UpdateById(int id, PetResponse model);
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
    var pets = _context.Pets;
    return _mapper.Map<IList<PetResponse>>(pets);
  }

  // Get a single Pet by id
  public PetResponse GetById(int id)
  {
    var pet = getPet(id);
    return _mapper.Map<PetResponse>(pet);
  }

  public void RegisterPet(RegisterPetRequest model, string origin)
  {
    var pet = _mapper.Map<Pet>(model);

    _context.Pets.Add(pet);
    _context.SaveChanges();
  }

  public PetResponse UpdateById(int id, PetResponse model)
  {
    var pet = getPet(id);

    _mapper.Map(model, pet);
    _context.Pets.Update(pet);
    _context.SaveChanges();

    return _mapper.Map<PetResponse>(pet);
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
    var pet = _context.Pets.Find(id);

    if (pet == null) throw new KeyNotFoundException("Pet not found");

    return pet;
  }
}