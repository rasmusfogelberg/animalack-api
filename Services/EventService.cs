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
    var events = _context.Events;
    _context.Events.Include(e => e.Pet).ToList();

    return _mapper.Map<IList<EventResponse>>(events);
  }

  // Get a single Event by id
  public SingleEventResponse GetById(int id)
  {
    var @event = getEvent(id);
    return _mapper.Map<SingleEventResponse>(@event);
  }

  public void AddEvent(AddEventRequest model, string origin)
  {

    var pet = _context.Pets.Find(model.Pet);
    if (pet == null) throw new KeyNotFoundException("Pet not found");


    var @event = new Event
    {
      Pet = pet,
      Name = model.Name,
      Type = model.Type,
      StartsAt = model.StartsAt,
      EndsAt = model.EndsAt
    };

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
    _context.Events.Include(e => e.Pet).ToList();

    if (@event == null) throw new KeyNotFoundException("Event not found");

    return @event;
  }
}