namespace CleanArchitucure.Application.Interfaces.Services;

public interface IMealService
{
	Task<Result<Meal>> AddAsync(CreateMealRequest request, CancellationToken cancellationToken = default);
	Task<Result<MealResponse>> GetMeal(string mealId, CancellationToken cancellationToken = default);
}