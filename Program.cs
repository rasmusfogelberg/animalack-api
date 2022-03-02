using AnimalackApi.Helpers;
using AnimalackApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
  var services = builder.Services;
  var env = builder.Environment;

  services.AddDbContext<DataContext>();
  services.AddCors();
  services.AddControllers();
  services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
  services.AddSwaggerGen();
  services.AddEndpointsApiExplorer();

  services.AddScoped<IUserService, UserService>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
