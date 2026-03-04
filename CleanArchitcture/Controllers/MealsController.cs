namespace CleanArchitcture.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MealsController(IMealService mealService) : ControllerBase
{
	private readonly IMealService _mealService = mealService;

	[HttpPost("add")]
	public async Task<IActionResult> Add(CreateMealRequest request, CancellationToken cancellationToken = default)
	{
		var result = await _mealService.AddAsync(request, cancellationToken);
		
		return result.IsSuccess ? Ok() : result.ToProblem();
	}

	[HttpGet("{mealId}")]
	public async Task<IActionResult> GetMeal(string mealId, CancellationToken cancellationToken = default)
	{
		var result = await _mealService.GetMeal(mealId, cancellationToken);
		
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
}