namespace AllSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class FavoritesController : ControllerBase
  {
    private readonly FavoritesService _favService;
    private readonly Auth0Provider _a0;

    public FavoritesController(Auth0Provider a0, FavoritesService favService)
    {
      _a0 = a0;
      _favService = favService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Favorite>> Create([FromBody] Favorite favoriteData)
    {
      try
      {
        Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
        favoriteData.AccountId = userInfo.Id;
        Favorite favorite = _favService.Create(favoriteData);
        return Ok(favorite);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{favoriteId}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int favoriteId)
    {
      try
      {
        Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
        string message = _favService.Delete(favoriteId, userInfo.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}