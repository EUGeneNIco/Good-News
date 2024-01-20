using GoodNewsApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace NewsApi
{
  public class Program
  {
    static void Main()
    {
      const string connectionString = "Server=.\\sqlexpress;Database=GoodNews;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";

      var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
      optionsBuilder.UseSqlServer(connectionString);

      using var dbContext = new AppDbContext(optionsBuilder.Options);
      var newsUpdate = new NewsUpdate(dbContext);

      newsUpdate.Start();
    }
  }
}
