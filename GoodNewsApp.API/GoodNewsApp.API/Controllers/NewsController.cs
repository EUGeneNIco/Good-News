using GoodNewsApp.API.Dto;
using GoodNewsApp.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace GoodNewsApp.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class NewsController : ControllerBase
  {
    private readonly AppDbContext dbContext;

    public NewsController(AppDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    [HttpGet("getNews/{category}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetNews(string category)
    {
      try
      {
        if (category == "headlines")
          category = "";

        var dbNews = dbContext.News
          .Where(x => x.Category == category)
          .ToList();

        var newsList = dbNews
          .Where(n => DateTime.TryParse(n.PublishedAt, out var date))
          .OrderByDescending(n => DateTime.Parse(n.PublishedAt))
          .Select(n => new NewsDto
          {
            Category = n.Category,
            Author = n.Author,
            Content = n.Content,
            Description = n.Description,
            PublishedAt = MapToWordValue(n.PublishedAt),
            Title = n.Title,
            Url = n.Url,
            UrlToImage = n.UrlToImage,
            SourceName = n.SourceName,
          })
          .ToList();

        return Ok(newsList);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex);
      }
    }

    private string MapToWordValue(string publishedAt)
    {
      DateTime? publishedAtDateTime = DateTime.TryParse(publishedAt, out var date) ? date : null;

      var timeSpan = DateTime.Now - publishedAtDateTime;
      var hours = 0;
      if (timeSpan.HasValue)
        hours = Convert.ToInt32(timeSpan.Value.TotalHours);

      if (hours == 0) return null;

      if (hours <= 24)
      {
        return $"{hours}h ago";
      }
      else
      {
        var days = Convert.ToInt32(timeSpan.Value.TotalDays);
        return $"{days}d ago";
      }
    }
  }
}
