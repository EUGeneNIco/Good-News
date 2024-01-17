using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApi
{
  public class NewsUpdate
  {
    private readonly string API_KEY = "9027b4a82865458685a9738be2a7ac5d";
    private readonly List<string> CategoryList = NewsCategory.GetAllCategories();

    public async void Start()
    {
      try
      {
        using var httpClient = new HttpClient();

        foreach (var cat in CategoryList)
        {
          var categoryParam = !string.IsNullOrEmpty(cat)
            ? $"?category={cat}&"
            : "?";

          var categoryUrl = $"https://newsapi.org/v2/top-headlines{categoryParam}country=us&sortBy=popularity&apiKey={API_KEY}";

          var responseMessage = httpClient.GetAsync(categoryUrl);

          var result = responseMessage.Result;

          if (result.IsSuccessStatusCode)
          {
            var response = await result.Content.ReadAsStringAsync();
            var newsListResponse = JsonConvert.DeserializeObject<NewsApiResponse>(response);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

  }

  public class NewsCategory
  {
    public const string Headline = "";
    public const string Sports = "sports";
    public const string Technology = "technology";
    public const string Business = "bussiness";
    public const string Entertainment = "entertainment";
    public const string Health = "health";
    public const string Science = "science";

    public static List<string> GetAllCategories()
    {
      return new List<string>
      {
        NewsCategory.Headline,
        NewsCategory.Sports,
        NewsCategory.Technology,
        NewsCategory.Business,
        NewsCategory.Entertainment,
        NewsCategory.Health,
        NewsCategory.Science,
      };
    }
  }

  public class NewsApiResponse
  {
    public string Status { get; set; }
    public int TotalResults { get; set; }
    public List<ArticleResponse> Articles { get; set; }
  }

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

  public class ArticleSourceResponse
  {
    public string Id { get; set; }
    public string Name { get; set; }
  }
}
