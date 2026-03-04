namespace CleanArchitucure.Application.Contracts.Meals.Requests;

public record CreateMealRequest
(
	string Name,
	string Description,
	decimal Price,
	IEnumerable<CreateMealOptionRequest> Options
);