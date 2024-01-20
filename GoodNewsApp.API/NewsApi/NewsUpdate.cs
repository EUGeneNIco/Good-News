using GoodNewsApp.Domain.Entities;
using GoodNewsApp.Persistence;
using NewsApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NewsApi
{
  public class NewsUpdate
  {

    private readonly string API_KEY = "9027b4a82865458685a9738be2a7ac5d";
    private readonly List<string> CategoryList = NewsCategoryEnums.GetAllCategories();
    private readonly AppDbContext dbContext;

    public NewsUpdate(AppDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public async void Start()
    {
      try
      {
        foreach (var cat in CategoryList)
        {
          var httpClient = new HttpClient();

          var categoryParam = !string.IsNullOrEmpty(cat)
            ? $"?category={cat}&"
            : "?";

          var categoryUrl = $"https://newsapi.org/v2/top-headlines{categoryParam}country=us&sortBy=popularity";

          httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");
          httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GoodNewsApp", "1.0"));

          var responseMessage = httpClient.GetAsync(categoryUrl);

          var result = responseMessage.Result;

          var response = await result.Content.ReadAsStringAsync();
          if (result.IsSuccessStatusCode)
          {
            var newsListResponse = JsonConvert.DeserializeObject<NewsApiResponse>(response);

            if (newsListResponse is null)
              continue;

            await SaveNewsToDb(newsListResponse.Articles, cat);
          }
          else
          {
            var errorResponse = JsonConvert.DeserializeObject<NewsApiErrorResponse>(response);

            Console.WriteLine(string.Format("Category: {0}, Status code: {1}, Reason: {2}", GetCategoryName(cat), errorResponse.status, errorResponse.message));
          }
        }

        this.dbContext.SaveChanges();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private string GetCategoryName(string cat)
    {
      return string.IsNullOrEmpty(cat) ? "headlines" : cat;
    }

    private bool IsArticleDetailsComplete(ArticleResponse article)
    {
      return !string.IsNullOrEmpty(article.Description) &&
        !string.IsNullOrEmpty(article.Author) &&
        !string.IsNullOrEmpty(article.Title) &&
        !string.IsNullOrEmpty(article.Url) &&
        !string.IsNullOrEmpty(article.UrlToImage);
    }

    private async Task SaveNewsToDb(List<ArticleResponse> articles, string category)
    {
      if (articles is null || !articles.Any())
      {
        Console.WriteLine($"No articles to save for {GetCategoryName(category)} category.");
        return;
      }

      articles = FilterArticles(articles);

      if (!articles.Any())
      {
        Console.WriteLine($"No articles to save for {GetCategoryName(category)} category (after filtering).");
        return;
      }

      this.dbContext.AddRange(articles.Select(art => new News
      {
        Category = category,
        Author = art.Author,
        Content = art.Content,
        Description = art.Description,
        PublishedAt = art.PublishedAt,
        Title = art.Title,
        Url = art.Url,
        UrlToImage = art.UrlToImage,
        SourceId = art.Source.Id,
        SourceName = art.Source.Name,
      }).ToList());

      Console.WriteLine(string.Format("Successfully saved {0} articles on {1} category", articles.Count, GetCategoryName(category)));
    }

    private List<ArticleResponse> FilterArticles(List<ArticleResponse> articles)
    {

      // Filter out some incomplete news article 
      articles = articles
        .Where(a => IsArticleDetailsComplete(a))
        .ToList();

      var dbArticlesUrls = this.dbContext.News
        .Select(n => n.Url)
        .ToList();

      if (dbArticlesUrls.Any())
      {
        //Filter out repeating News
        articles = articles
          .Where(a => !dbArticlesUrls.Contains(a.Url))
          .ToList();
      }

      return articles;
    }
  }
}
