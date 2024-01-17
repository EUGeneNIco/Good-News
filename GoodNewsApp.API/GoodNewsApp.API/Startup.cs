using GoodNewsApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GoodNewsApp.API
{
  public class Startup
  {
    private readonly IConfiguration Configuration;
    private readonly string CLIENT_URL;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      this.CLIENT_URL = Configuration["ApplicationSettings:Client_URL"].ToString();
    }
    public void ConfigureServices(IServiceCollection services)
    {
      // DB
      services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
        //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

      }

      app.UseHttpsRedirection();

      app.UseRouting();

      // CORS
      app.UseCors(builder =>
          builder.WithOrigins(this.CLIENT_URL)
          .AllowAnyHeader()
          .AllowAnyMethod()
      );

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
