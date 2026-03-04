namespace CleanArchitucure.Application.Contracts.MealOptions.Validators;

public class CreateMealOptionValidator : AbstractValidator<CreateMealOptionRequest>
{
	public CreateMealOptionValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MinimumLength(2)
			.MaximumLength(30);

		RuleFor(x => x.DisplayOrder)
			.GreaterThanOrEqualTo(1)
			.LessThanOrEqualTo(25);

		RuleFor(x => x.Items)
			.Must(x => 
				(x.Any() && x.Count() <= 2) &&
				(x.Count() == x.DistinctBy(x => x.DisplayOrder).Count()) &&
				(x.Count() == x.DistinctBy(x => x.Name).Count())
			)
			.When(x => x.Items is not null && x.Items.Any())
			.WithMessage("Option items must be between 1 and 20 with unique names and display orders.");

		RuleForEach(x => x.Items)
			.SetValidator(new CreateOptionItemValidator())
			.When(x => x.Items is not null && x.Items.Any());
	}
}