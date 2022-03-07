namespace AnimalackApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AnimalackApi.Entities;
using AnimalackApi.Helpers;
using AnimalackApi.Services;
using AnimalackApi.Models.Pets;

[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
  private readonly IPetService _petService;

  public PetsController(IPetService petService)
  {
    _petService = petService;
  }

  // Get all Pets
  [HttpGet]
  public ActionResult<IEnumerable<Pet>> GetPets()
  {
    var pets = _petService.GetAll();
    return Ok(pets);
  }

  // Get a single Pet
  [HttpGet("{id}")]
  public ActionResult<Pet> GetPet(int id)
  {
    var pet = _petService.GetById(id);

    if (pet == null)
    {
      return NotFound();
    }

    return Ok(pet);
  }

  // Edit a pet
  [HttpPut("{id}")]
  public ActionResult<SinglePetResponse> UpdatePet(int id, UpdatePetRequest model)
  {
    var pet = _petService.UpdateById(id, model);

    return Ok(new { message = "Pet successfully updated!" });
  }

  // Register a new Pet
  [HttpPost("register")]
  public ActionResult<Pet> Register(RegisterPetRequest model)
  {
    _petService.RegisterPet(model, Request.Headers["origin"]);

    return Ok(new { message = "Pet successfully registered!" });
  }

  // Delete a specific Pet
  [HttpDelete("{id}")]
  public IActionResult DeletePet(int id)
  {
    _petService.DeleteById(id);

    return Ok(new { message = "Pet successfully deleted!" });
  }
}
