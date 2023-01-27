namespace AllSpice.Repositories
{
  public class FavoritesRepository
  {
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
      _db = db;
    }

    public Favorite Create(Favorite favoriteData)
    {
      string sql = @"
      INSERT INTO mcfavorites
      (accountId, recipeId)
      VALUES
      (@accountId, @recipeId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, favoriteData);
      favoriteData.Id = id;
      return favoriteData;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM mcfavorites WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }

    internal List<MyRecipes>  GetByAccountId(string accountId)
    {
      string sql = @"
      SELECT
      r.*,
      f.*,
      a.*
      FROM mcrecipes r
      JOIN mcfavorites f ON f.recipeId = r.id
      JOIN accounts a ON f.accountId = a.id
      WHERE f.accountId = @accountId;
      ";
      return _db.Query<MyRecipes, Favorite, Profile, MyRecipes>(sql, (myRecipes, favorite, profile) =>
      {
        myRecipes.FavoriteId = favorite.Id;
        myRecipes.Creator = profile;
        return myRecipes;
      }, new { accountId }).ToList();
    }

    internal Favorite GetById(int favoriteId)
    {
      string sql = @"
      SELECT
      f.*,
      a.*
      FROM mcfavorites f
      JOIN accounts a ON f.accountId = a.id
      WHERE f.id = @favoriteId;
      ";
      return _db.Query<Favorite, Profile, Favorite>(sql, (favorite, profile) =>
      {
        favorite.Creator = profile;
        return favorite;
      }, new { favoriteId }).FirstOrDefault();
    }
  }
}