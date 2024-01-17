using GoodNewsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodNewsApp.Persistence
{
  public class AppDbContext : DbContext
  {
    public DbSet<News> News { get; set; }
    public DbSet<NewsSource> NewsSources { get; set; }
  }
}
