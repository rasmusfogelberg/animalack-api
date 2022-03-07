using AnimalackApi.Entities;
using AnimalackApi.Helpers;
using AnimalackApi.Models.Events;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AnimalackApi.Services;

public interface IEventService
{
  void AddEvent(AddEventRequest model, string origin);
  IEnumerable<EventResponse> GetAll();
  SingleEventResponse GetById(int id);
  SingleEventResponse UpdateById(int id, UpdateEventRequest model);
  void DeleteById(int id);
}

// Class
public class EventService : IEventService
{
  private readonly DataContext _context;
  private readonly IMapper _mapper;

  // Constructor
  public EventService(DataContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  // Get all Events
  public IEnumerable<EventResponse> GetAll()
  {
    // TODO: Include Pets and extend to users so they can be listed with the Event
    var events = _context.Events;
    return _mapper.Map<IList<EventResponse>>(events);
  }

  // Get a single Event by id
  public SingleEventResponse GetById(int id)
  {
    var @event = getEvent(id);
    return _mapper.Map<SingleEventResponse>(@event);
  }

  // TODO: Look over last how to implement this when I know what is needed
  public void AddEvent(AddEventRequest model, string origin)
  {
    /*  List<User> users = new List<User>();

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
     _context.SaveChanges(); */
  }

  public SingleEventResponse UpdateById(int id, UpdateEventRequest model)
  {
    var @event = getEvent(id);

    _mapper.Map(model, @event);
    _context.Events.Update(@event);
    _context.SaveChanges();

    return _mapper.Map<SingleEventResponse>(@event);
  }

  public void DeleteById(int id)
  {
    var @event = getEvent(id);

    _context.Events.Remove(@event);
    _context.SaveChanges();
  }

  /* Helper methods */
  private Event getEvent(int id)
  {
    var @event = _context.Events.Find(id);

    if (@event == null) throw new KeyNotFoundException("Event not found");

    return @event;
  }
}