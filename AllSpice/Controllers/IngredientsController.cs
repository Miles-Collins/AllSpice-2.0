namespace AllSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class IngredientsController : ControllerBase
  {
    private readonly IngredientsService _inService;
    private readonly Auth0Provider _a0;

    public IngredientsController(Auth0Provider a0, IngredientsService inService)
    {
      _a0 = a0;
      _inService = inService;
    }

    [HttpGet]
    public ActionResult<List<Ingredient>> Get()
    {
      try
      {
        List<Ingredient> ingredients = _inService.Get();
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Ingredient> GetById(int id)
    {
      try
      {
        Ingredient ingredient = _inService.GetById(id);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData)
    {
      try
      {
        Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
        ingredientData.CreatorId = userInfo.Id;
        Ingredient ingredient = _inService.Create(ingredientData);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Update([FromBody] Ingredient ingredientData, int id)
    {
      try
      {
        Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
        ingredientData.Id = id;
        Ingredient updatedIngredient = _inService.Update(ingredientData, userInfo.Id);
        return updatedIngredient;
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
        string message = _inService.Delete(id, userInfo.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}