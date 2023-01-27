namespace AllSpice.Models
{
  public class Recipe : DbItem<int>
  {
    public string Title { get; set; }
    public string Instructions { get; set; }
    public string Img { get; set; }
    public string Category { get; set; }
    public Profile Creator { get; set; }
    public Ingredient Ingredients { get; set; }
    public string CreatorId { get; set; }
  }

  public class MyRecipes : Recipe
  {
    public int FavoriteId { get; set; }
  }
}