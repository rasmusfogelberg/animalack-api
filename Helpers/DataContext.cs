using AnimalackApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalackApi.Helpers;

public class DataContext : DbContext
{
  public DbSet<User> Users { get; set; }
  public DbSet<Pet> Pets { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>()
                .HasMany(u => u.Pets);
    
    modelBuilder.Entity<Pet>()
                .HasOne(p => p.User);
  }

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