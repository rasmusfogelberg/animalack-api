namespace AnimalackApi.Controllers;

using AnimalackApi.Entities;
using Microsoft.AspNetCore.Mvc;

[Controller]
public abstract class AbstractController : ControllerBase
{
  public User AuthenticatedUser => (User)HttpContext.Items["AuthenticatedUser"];
}