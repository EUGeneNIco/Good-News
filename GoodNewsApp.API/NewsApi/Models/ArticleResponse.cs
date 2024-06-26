namespace NewsApi.Models
{
  public class ArticleResponse
  {
    public string Author { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
    public string PublishedAt { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string UrlToImage { get; set; }
    public ArticleSourceResponse Source { get; set; }
  }
}
