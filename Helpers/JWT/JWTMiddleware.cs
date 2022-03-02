using AnimalackApi.Helpers.JWT;

namespace AnimalackApi.Helpers.JWT;

public class JWTMiddleware
{
  private readonly RequestDelegate _next;
  public JWTMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext httpContext, DataContext dataContext, IJWTUtils jwtUtils)
  {
    var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    var userId = jwtUtils.ValidateToken(token);
    if (userId != null)
    {
      // attach user to context on successful jwt validation
      httpContext.Items["AuthenticatedUser"] = await dataContext.Users.FindAsync(userId.Value);
    }

    await _next(httpContext);
  }
}