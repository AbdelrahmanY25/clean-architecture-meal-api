namespace CleanArchitucure.Application.Contracts.OptionItems.Requests;

public record CreateOptionItemRequest
(
	string Name,
	decimal Price,
	int DisplayOrder,
	bool IsPobular
);