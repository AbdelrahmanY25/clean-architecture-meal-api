namespace CleanArchitucure.Application.Contracts.OptionItems.Validators;

public class CreateOptionItemValidator : AbstractValidator<CreateOptionItemRequest>
{
	public CreateOptionItemValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MinimumLength(2)
			.MaximumLength(30);

		RuleFor(x => x.Price)
			.GreaterThanOrEqualTo(0);

		RuleFor(x => x.DisplayOrder)
			.GreaterThanOrEqualTo(1)
			.LessThanOrEqualTo(20);
	}
}