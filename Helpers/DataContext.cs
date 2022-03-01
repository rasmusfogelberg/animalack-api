using AnimalackApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnmialackApi.Helpers;

public class DataContext : DbContext
{
  public DbSet<User> Users { get; set; }
  private readonly IConfiguration Configuration;

  public DataContext(IConfiguration configuration)
  {
    Configuration = configuration;
  }
  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    options.UseSqlServer(Configuration.GetConnectionString("AnimalackApiDatabase"));
  }

}