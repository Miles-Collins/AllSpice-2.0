namespace AllSpice.Services
{
  public class FavoritesService
  {
    private readonly FavoritesRepository _favRepo;
    private readonly RecipeService _recipeService;
    public FavoritesService(FavoritesRepository favRepo, RecipeService recipeService)
    {
      _favRepo = favRepo;
      _recipeService = recipeService;
    }

    internal Favorite Create(Favorite favoriteData)
    {
      // Recipe recipe = _recipeService.GetById(recipeId);
      // favoriteData.recipeId = recipe.Id;
      Favorite favorite = _favRepo.Create(favoriteData);
      return favorite;
    }

    internal Favorite GetById(int favoriteId)
    {
      Favorite favorite = _favRepo.GetById(favoriteId);
      if (favorite == null)
      {
        throw new Exception("Does not exist.");
      }
      return favorite;
    }

    internal string Delete(int favoriteId, string userId)
    {
      Favorite favorite = GetById(favoriteId);
      if (favorite.Creator.Id != userId)
      {
        throw new Exception("Id error.");
      }
      Recipe recipe = _recipeService.GetById(favorite.recipeId);
      _favRepo.Delete(favorite.Id);
      return $"You have unfavorite {recipe.Title}.";
    }

    internal List<MyRecipes> GetByAccountId(string userId)
    {
      List<MyRecipes> favorite = _favRepo.GetByAccountId(userId);
      if (favorite == null)
      {
        throw new Exception("That user does not exist.");
      }
      return favorite;
    }

    internal List<MyRecipes> GetFavorite(string userId)
    {
      List<MyRecipes> favorite = GetByAccountId(userId);
      return favorite;
    }
  }
}