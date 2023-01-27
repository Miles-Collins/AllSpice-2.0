namespace AllSpice.Services
{
  public class RecipeService
  {

    private readonly RecipeRepository _recipeRepo;

    public RecipeService(RecipeRepository recipeRepo)
    {
      _recipeRepo = recipeRepo;
    }

    internal List<Recipe> Get()
    {
      List<Recipe> recipes = _recipeRepo.Get();
      return recipes;
    }

    internal Recipe GetById(int id)
    {
      Recipe recipe = _recipeRepo.GetById(id);
      if (recipe == null)
      {
        throw new Exception("There isn't a recipe with that Id.");
      }
      return recipe;
    }
    internal Recipe Create(Recipe recipeData)
    {
      Recipe recipe = _recipeRepo.Create(recipeData);
      return recipe;
    }

    internal Recipe Update(Recipe recipeData, string userId)
    {
      Recipe originalRecipe = GetById(recipeData.Id);
      if (originalRecipe.CreatorId != userId)
      {
        throw new Exception("You do not have permission to edit " + originalRecipe.Title + ".");
      }
      originalRecipe.Category = recipeData.Category ?? originalRecipe.Category;
      originalRecipe.UpdatedAt = recipeData.UpdatedAt;
      originalRecipe.Img = recipeData.Img ?? originalRecipe.Img;
      originalRecipe.Instructions = recipeData.Instructions ?? originalRecipe.Instructions;
      originalRecipe.Title = recipeData.Title ?? originalRecipe.Title;

      Recipe updatedRecipe = _recipeRepo.Update(originalRecipe);
      return updatedRecipe;
    }

    internal string Delete(int recipeId, string userId)
    {
      Recipe recipe = GetById(recipeId);
      if (recipe.CreatorId != userId)
      {
        throw new Exception("You do not have permission to delete " + recipe.Title + ".");
      }
      _recipeRepo.Delete(recipe.Id);
      string message = ("You have successfully deleted " + recipe.Title + ".");
      return message;
    }
  }
}