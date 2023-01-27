using AllSpice.Interfaces;

namespace AllSpice.Repositories
{
  public class IngredientsRepository : BaseRepo, IRepository<Ingredient, int>
  {
    public IngredientsRepository(IDbConnection db) : base(db)
    {
    }

    public Ingredient Create(Ingredient ingredientData)
    {
      string sql = @"
      INSERT INTO mcingredients
      (name, quantity, recipeId, creatorId)
      VALUES
      (@name, @quantity, @recipeId, @creatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, ingredientData);
      ingredientData.Id = id;
      return ingredientData;
    }

    public void Delete(int id)
    {
      string sql = @"
      DELETE FROM mcingredients WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }

    public List<Ingredient> Get()
    {
      throw new NotImplementedException();
    }

    public Ingredient GetById(int id)
    {
      string sql = @"
      SELECT 
      i.*,
      a.*
      FROM mcingredients i
      JOIN accounts a ON i.creatorId = a.id
      WHERE i.id = @id;
      ";
      return _db.Query<Ingredient, Profile, Ingredient>(sql, (ingredient, profile) =>
      {
        ingredient.Creator = profile;
        return ingredient;
      }, new { id }).FirstOrDefault();
    }

    public List<Ingredient> GetByRecipeId(int id)
    {
      string sql = @"
      SELECT
      i.*,
      a.*,
      r.*
      FROM mcingredients i
      JOIN accounts a ON i.creatorId = a.id
      JOIN mcrecipes r ON i.recipeId = r.id
      WHERE i.recipeId = @id;
      ";
      return _db.Query<Ingredient, Profile, Recipe, Ingredient>(sql, (ingredient, profile, recipe) =>
      {
        ingredient.Creator = profile;
        ingredient.RecipeId = recipe.Id;
        return ingredient;
      }, new { id }).ToList();
    }

    public Ingredient Update(Ingredient newData)
    {
      throw new NotImplementedException();
    }


  }
}