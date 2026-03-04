namespace CleanArchitucure.Application.Contracts.MealOptions.Responses;

public record MealOptionsResponse
(
	string Id,
	string Name,
	int DisplayOrder,
	IReadOnlyCollection<OptionItemsResponse> Items
);