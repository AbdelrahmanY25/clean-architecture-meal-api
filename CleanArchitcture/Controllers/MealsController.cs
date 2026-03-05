using CleanArchitucure.Application.Contracts.Meals.Response;
using Mapster;

namespace CleanArchitcture.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MealsController(IMealService mealService) : ControllerBase
{
	private readonly IMealService _mealService = mealService;

	[HttpPost("add")]
	public async Task<IActionResult> Add([FromBody] CreateMealRequest request, CancellationToken cancellationToken = default)
	{
		var result = await _mealService.AddAsync(request, cancellationToken);
		
		return result.IsSuccess ?
			CreatedAtAction(nameof(GetMeal), new { mealId = result.Value.Id  }, result.Value) : result.ToProblem();
	}

	[HttpGet("{mealId}")]
	public async Task<IActionResult> GetMeal([FromRoute] string mealId, CancellationToken cancellationToken = default)
	{
		var result = await _mealService.GetMeal(mealId, cancellationToken);
		
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
	{
		var result = await _mealService.GetAll(cancellationToken);
		
		return Ok(result);
	}
}