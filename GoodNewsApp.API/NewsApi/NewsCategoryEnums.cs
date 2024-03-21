namespace NewsApi
{
  public class NewsCategoryEnums
  {
    public const string Headline = "";
    public const string Sports = "sports";
    public const string Technology = "technology";
    public const string Business = "business";
    public const string Entertainment = "entertainment";
    public const string Health = "health";
    public const string Science = "science";

    public static List<string> GetAllCategories()
    {
      return new List<string>
      {
        NewsCategoryEnums.Headline,
        NewsCategoryEnums.Sports,
        NewsCategoryEnums.Technology,
        NewsCategoryEnums.Business,
        NewsCategoryEnums.Entertainment,
        NewsCategoryEnums.Health,
        NewsCategoryEnums.Science,
      };
    }
  }
}
