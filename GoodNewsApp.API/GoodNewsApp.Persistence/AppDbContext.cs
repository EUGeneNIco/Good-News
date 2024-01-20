using GoodNewsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodNewsApp.Persistence
{
  public class AppDbContext : DbContext
  {
    public DbSet<News> News { get; set; }

    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      //modelBuilder.RemovePluralizingTableNameConvention();
    }
  }
}
