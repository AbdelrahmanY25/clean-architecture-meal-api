namespace CleanArchitucure.Application.Contracts.MealOptions.Requests;

public record CreateMealOptionRequest
(
	string Name,
	int DisplayOrder,
	IEnumerable<CreateOptionItemRequest> Items
);