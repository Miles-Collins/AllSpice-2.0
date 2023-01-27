using AllSpice.Interfaces;

namespace AllSpice.Services
{
  public class IngredientsService : BaseService, IService<Ingredient, int>
  {
    private readonly IngredientsRepository _inRepo;
    private readonly RecipeService _recipeService;
    public IngredientsService(IDbConnection db, IngredientsRepository inRepo, RecipeService recipeService) : base(db)
    {
      _inRepo = inRepo;
      _recipeService = recipeService;
    }

    public Ingredient Create(Ingredient ingredientData)
    {
      Ingredient ingredient = _inRepo.Create(ingredientData);
      return ingredient;
    }


    public string Delete(int id, string userId)
    {
      Ingredient ingredient = GetById(id);
      // if (ingredient.CreatorId != userId)
      // {
      //   throw new Exception($"You do not have permission to delete {ingredient.Name}.");
      // }
      _inRepo.Delete(ingredient.Id);
      string message = $"You have successfully deleted {ingredient.Name}.";
      return message;
    }

    public Ingredient GetById(int id)
    {
      Ingredient ingredient = _inRepo.GetById(id);
      if (ingredient == null)
      {
        throw new Exception("That ingredient does not exist.");
      }
      return ingredient;
    }

    public List<Ingredient> Get()
    {
      List<Ingredient> ingredients = _inRepo.Get();
      return ingredients;
    }


    public List<Ingredient> GetByRecipeId(int recipeId)
    {
      Recipe recipe = _recipeService.GetById(recipeId);
      if (recipe == null)
      {
        throw new Exception("Could not find that recipe in the database");
      }
      List<Ingredient> ingredient = _inRepo.GetByRecipeId(recipeId);
      return ingredient;
    }

    public Ingredient Update(Ingredient data, string id)
    {
      throw new NotImplementedException();
    }

    // public Ingredient Update(Ingredient ingredientData, string userId)
    // {
    //   Ingredient originalIngredient = GetById(ingredientData.Id);
    //   if (originalIngredient.CreatorId != userId)
    //   {
    //     throw new Exception();
    //   }
    //   originalIngredient.Name = ingredientData.Name ?? originalIngredient.Name;
    //   originalIngredient.Quantity = ingredientData.Quantity;
    //   Ingredient newIngredient = _inRepo.Update(originalIngredient);
    //   return newIngredient;
    // }
  }
}
