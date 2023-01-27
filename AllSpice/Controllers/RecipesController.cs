namespace AllSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
    private readonly RecipeService _recipeService;
    private readonly Auth0Provider _a0;
    private readonly IngredientsService _ingService;
    private readonly FavoritesService _favService;

    public RecipesController(Auth0Provider a0, RecipeService recipeService, FavoritesService favService, IngredientsService ingService)
    {
      _a0 = a0;
      _recipeService = recipeService;
      _favService = favService;
      _ingService = ingService;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> Get()
    {
      try
      {
        List<Recipe> recipes = _recipeService.Get();
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Recipe> GetById(int id)
    {
      try
      {
        Recipe recipe = _recipeService.GetById(id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
    {
      try
      {
        Account userId = await _a0.GetUserInfoAsync<Account>(HttpContext);
        recipeData.CreatorId = userId.Id;
        Recipe recipe = _recipeService.Create(recipeData);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> Update([FromBody] Recipe recipeData, int id)
    {
      try
      {
        Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
        recipeData.Id = id;
        Recipe recipe = _recipeService.Update(recipeData, userInfo.Id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
        string message = _recipeService.Delete(id, userInfo.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION INGREDIENTS \\

    [HttpGet("{id}/ingredients")]
    public ActionResult<List<Ingredient>> GetIngredientById(int id)
    {
      try
      {
        List<Ingredient> ingredient = _ingService.GetByRecipeId(id);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // SECTION FAVORITES \\

    // [HttpPost("{id}/favorites")]
    // [Authorize]
    // public async Task<ActionResult<Favorite>> Create([FromBody] Favorite favoriteData, int recipeId)
    // {
    //   try
    //   {
    //     Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
    //     favoriteData.AccountId = userInfo.Id;
    //     Favorite favorite = _favService.Create(recipeId, favoriteData);
    //     return Ok(favorite);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
  }
}
