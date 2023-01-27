using AllSpice.Interfaces;

namespace AllSpice.Repositories
{
  public class RecipeRepository : BaseRepo, IRepository<Recipe, int>
  {
    public RecipeRepository(IDbConnection db) : base(db)
    {
    }

    public Recipe Create(Recipe recipeData)
    {
      string sql = @"
      INSERT INTO mcrecipes
      (title, instructions, img, category, creatorId)
      VALUES
      (@title, @instructions, @img, @category, @creatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, recipeData);
      recipeData.Id = id;
      return recipeData;
    }

    public void Delete(int id)
    {
      string sql = @"
      DELETE FROM mcrecipes WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }

    public List<Recipe> Get()
    {
      string sql = @"
      SELECT
      r.*,
      a.*
      FROM mcrecipes r
      JOIN accounts a ON r.creatorId = a.Id;
      ";
      return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
      {
        recipe.Creator = profile;
        return recipe;
      }).ToList();
    }

    public Recipe GetById(int id)
    {
      string sql = @"
      SELECT
      r.*,
      i.*,
      a.*
      FROM mcrecipes r
      LEFT JOIN mcingredients i ON r.id = i.recipeId
      JOIN accounts a ON r.creatorId = a.Id
      WHERE r.id = @id;
      ";
      return _db.Query<Recipe, Ingredient, Profile, Recipe>(sql, (recipe, ingredient, profile) =>
      {
        recipe.Ingredients = ingredient;
        recipe.Creator = profile;
        return recipe;
      }, new { id }).FirstOrDefault();
    }

    public Recipe Update(Recipe newData)
    {
      string sql = @"
      UPDATE mcrecipes SET
      title = @title,
      instructions = @instructions,
      img = @img,
      category = @category,
      creatorId = @creatorId
      WHERE id = @id;
      ";
      _db.Execute(sql, newData);
      return newData;
    }
  }
}