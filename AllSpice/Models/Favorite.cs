namespace AllSpice.Models
{
  public class Favorite : DbItem<int>
  {
    public string AccountId { get; set; }
    public int recipeId { get; set; }
    public Profile Creator { get; set; }
  }
}