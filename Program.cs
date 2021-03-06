using System.Text.Json.Serialization;
using AnimalackApi.Helpers;
using AnimalackApi.Helpers.JWT;
using AnimalackApi.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
  var services = builder.Services;
  var env = builder.Environment;

  services.AddDbContext<DataContext>();
  services.AddCors();
  services.AddControllers().AddNewtonsoftJson(options =>
  {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
  });
  services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
  services.AddSwaggerGen();
  services.AddEndpointsApiExplorer();

  services.AddScoped<IUserService, UserService>();
  services.AddScoped<IJWTUtils, JWTUtils>();
  services.AddScoped<IPetService, PetService>();
  services.AddScoped<IEventService, EventService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// global error handler
app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseMiddleware<JWTMiddleware>();

 app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
