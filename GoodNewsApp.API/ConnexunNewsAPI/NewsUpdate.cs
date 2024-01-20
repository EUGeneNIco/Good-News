
//using GoodNewsApp.Domain.Entities;
//using GoodNewsApp.Persistence;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NewsApi
//{
//  public class NewsUpdate
//  {

//    private readonly string API_KEY = "9027b4a82865458685a9738be2a7ac5d";
//    private readonly List<string> CategoryList = NewsCategory.GetAllCategories();
//    private readonly AppDbContext dbContext;

//    public NewsUpdate(AppDbContext dbContext)
//    {
//      this.dbContext = dbContext;
//    }

//    public async void Start()
//    {
//      try
//      {
//        foreach (var cat in CategoryList)
//        {
//          using var httpClient = new HttpClient();

//          var categoryParam = !string.IsNullOrEmpty(cat)
//            ? $"?category={cat}&"
//            : "?";

//          var categoryUrl = $"https://newsapi.org/v2/top-headlines{categoryParam}country=us&sortBy=popularity";

//          httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");

//          var responseMessage = httpClient.GetAsync(categoryUrl);

//          var result = responseMessage.Result;

//          if (result.IsSuccessStatusCode)
//          {
//            var response = await result.Content.ReadAsStringAsync();
//            var newsListResponse = JsonConvert.DeserializeObject<CxnResponse>(response);

//            if (newsListResponse is null)
//              continue;

//            await SaveNewsToDb(newsListResponse.news, cat);
//          }
//          else
//          {
//            Console.WriteLine(string.Format("Category: {0}, Status code: {1}, Reason: {2}", cat, result.StatusCode, result.ReasonPhrase));
//          }
//        }
//      }
//      catch (Exception ex)
//      {
//        throw ex;
//      }
//    }

//    private bool IsArticleClean(CxnNews article)
//    {
//      return !string.IsNullOrEmpty(article.Description) &&
//        !string.IsNullOrEmpty(article.Source);
//    }

//    private async Task SaveNewsToDb(List<CxnNews> articles, string category)
//    {
//      if (articles is null || !articles.Any())
//        return;

//      // Filter out some incomplete news article 
//      articles = articles
//        .Where(a => IsArticleClean(a))
//        .ToList();

//      var dbArticlesUrls = this.dbContext.News
//        .Select(n => n.Url)
//        .ToList();

//      if (dbArticlesUrls.Any())
//      {
//        //Filter out repeating News
//        articles = articles
//          .Where(a => !dbArticlesUrls.Contains(a.Url))
//          .ToList();
//      }

//      this.dbContext.AddRange(articles.Select(art => new News
//      {
//        Category = category,
//        Author = art.Source,
//        Content = art.Description,
//        Description = art.Description,
//        PublishedAt = art.PublishedOn,
//        Title = art.Title,
//        Url = art.Url,
//        UrlToImage = art.Image,
//        SourceId = art.Source.Id,
//        SourceName = art.Source.Name,
//      }).ToList());

//      this.dbContext.SaveChanges();
//    }   
//  }

//  public class NewsCategory
//  {
//    public const string Headline = "";
//    public const string Sports = "sports";
//    public const string Technology = "technology";
//    public const string Business = "bussiness";
//    public const string Entertainment = "entertainment";
//    public const string Health = "health";
//    public const string Science = "science";

//    public static List<string> GetAllCategories()
//    {
//      return new List<string>
//      {
//        NewsCategory.Headline,
//        NewsCategory.Sports,
//        NewsCategory.Technology,
//        NewsCategory.Business,
//        NewsCategory.Entertainment,
//        NewsCategory.Health,
//        NewsCategory.Science,
//      };
//    }
//  }

//  public class CxnResponse
//  {
//    public CxnResult result { get; set; }
//    public List<CxnNews> news { get; set; }
//  }

//  public class CxnResult
//  {
//    public string response { get; set; }
//    public int newsCount { get; set; }
//    public int skipped { get; set; }
//  }

//  public class CxnNews
//  {
//    public string Title { get; set; }
//    public string Source { get; set; }
//    public string Url { get; set; }
//    public string PublishedOn { get; set; }
//    public string Description { get; set; }
//    public string Language { get; set; }
//    public string Image { get; set; }
//    public string SourceNationality { get; set; }
//    public CxnTitleSentiment TitleSentiment { get; set; }
//    public string Summary { get; set; }
//    public List<string> Countries { get; set; }
//    public CxnCategory Categories { get; set; }
//  }

//  public class CxnCategory
//  {
//    public string label { get; set; }
//    public string IPTCCode { get; set; }
//  }

//  public class CxnTitleSentiment
//  {
//    public string sentiment { get; set; }
//    public string score { get; set; }
//  }
//}
