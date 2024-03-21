namespace GoodNewsApp.API.Dto
{
  public class NewsDto
  {
    public string? Category { get; set; }
    public string Author { get; set; } // required
    public string? Content { get; set; }
    public string Description { get; set; } // required
    public string? PublishedAt { get; set; }
    public string Title { get; set; } // required
    public string Url { get; set; } // required
    public string UrlToImage { get; set; } // required
    public string? SourceId { get; set; }
    public string? SourceName { get; set; }
  }
}
