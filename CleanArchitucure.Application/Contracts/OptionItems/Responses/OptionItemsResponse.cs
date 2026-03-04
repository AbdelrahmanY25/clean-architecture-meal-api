namespace CleanArchitucure.Application.Contracts.OptionItems.Responses;

public record OptionItemsResponse
(
	string Id,
	string Name,
	decimal Price,
	int DisplayOrder,
	bool IsPobular
);