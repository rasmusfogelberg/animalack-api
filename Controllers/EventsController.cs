namespace AnimalackApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AnimalackApi.Entities;
using AnimalackApi.Helpers;
using AnimalackApi.Services;
using AnimalackApi.Models.Events;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
  private readonly IEventService _eventService;

  public EventsController(IEventService eventService)
  {
    _eventService = eventService;
  }

  // Get all Events
  [HttpGet]
  public ActionResult<IEnumerable<Event>> GetEvent()
  {
    var events = _eventService.GetAll();
    return Ok(events);
  }

  // Get a single Event
  [HttpGet("{id}")]
  public ActionResult<Event> GetEvent(int id)
  {
    var @event = _eventService.GetById(id);

    if (@event == null)
    {
      return NotFound();
    }

    return Ok(@event);
  }

  // Update an Event
  [HttpPut("{id}")]
  public ActionResult<SingleEventResponse> PutEvent(int id, UpdateEventRequest model)
  {
    var @event = _eventService.UpdateById(id, model);

    return Ok(new { message = "Event successfully updated!" });
  }

  // Add a new Event
  [HttpPost("add")]
  public ActionResult<Event> AddEvent(AddEventRequest model)
  {
   _eventService.AddEvent(model, Request.Headers["origin"]);

   return Ok(new { message = "Event Successfully added"});
  }

  // Delete a specific Event
  [HttpDelete("{id}")]
  public IActionResult DeleteEvent(int id)
  {
   _eventService.DeleteById(id);

   return Ok(new { message = "Event successfully deleted!"});
  }
}
