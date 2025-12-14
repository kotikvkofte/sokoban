using DBA.Entity;
using Microsoft.EntityFrameworkCore;

namespace DBA;

public class AppDbContext : DbContext
{
    public DbSet<PlayerEntity> Players { get; set; }
    public DbSet<LevelEntity> Levels { get; set; }
    public DbSet<StatisticEntity> Statistics { get; set; }

    public AppDbContext() : base()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=sokobanDb;Username=postgres;Password=1");
        base.OnConfiguring(optionsBuilder);
    }
}