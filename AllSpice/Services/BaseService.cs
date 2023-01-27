namespace AllSpice.Services
{
  public class BaseService
  {
    protected readonly IDbConnection _db;

    public BaseService(IDbConnection db)
    {
      _db = db;
    }
  }
}