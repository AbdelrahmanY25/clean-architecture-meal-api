namespace CleanArchitucure.Application.Contracts.Meals.Response;

public record MealResponse
(
	string Id,
	string Name,
	string Description,
	decimal Price,
	IReadOnlyCollection<MealOptionsResponse> Options
);