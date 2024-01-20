namespace NewsApi.Models
{
  public class NewsApiResponse
  {
    public string Status { get; set; }
    public int TotalResults { get; set; }
    public List<ArticleResponse> Articles { get; set; }
  }
}
