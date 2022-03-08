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
    // Why am I an idiot and makes a list? I CAN NOT add more than 1 pet anyway
    // redo this so it will only find the animal with the correct id
     List<Pet> pets = new List<Pet>();

  /*    foreach (int petId in model.Pets)
    {
      pets.AddRange(_context.Pets.Where(p => p.Id == petId).ToList());
    } */

     var @event = new Event
     {
       Pets = pets,
       Name = model.Name,
       Type = model.Type,
       StartsAt = model.StartsAt,
       EndsAt = model.EndsAt
     };

     var addEventResponse = _mapper.Map<AddEventResponse>(@event);

     _context.Events.Add(@event);
     _context.SaveChanges();
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