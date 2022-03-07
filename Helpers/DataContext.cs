using AnimalackApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalackApi.Helpers;

public class DataContext : DbContext
{
  public DbSet<User> Users { get; set; }
  public DbSet<Pet> Pets { get; set; }
  public DbSet<Event> Events { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>()
      .HasMany(u => u.Pets);

    modelBuilder.Entity<Pet>()
      .HasMany(p => p.Users);
      
    modelBuilder.Entity<Pet>()
      .Property(p => p.Gender)
      .HasConversion(
        v => v.ToString(),
        v => (PetGender)Enum.Parse(typeof(PetGender), v));

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
  public DbSet<AnimalackApi.Entities.Event> Event { get; set; }

}