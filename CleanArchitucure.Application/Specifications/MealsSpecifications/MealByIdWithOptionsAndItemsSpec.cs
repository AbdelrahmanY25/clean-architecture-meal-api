namespace CleanArchitucure.Application.Specifications.MealsSpecifications;

public sealed class MealByIdWithOptionsAndItemsSpec : Specification<Meal, MealResponse>
{
	public MealByIdWithOptionsAndItemsSpec(string mealId)
	{
		Criteria = m => m.Id == mealId;

		Selector = m => new MealResponse(
			m.Id,
			m.Name,
			m.Description,
			m.Price,
			m.MealOptionGroups.Select(g => new MealOptionsResponse(
				g.Id,
				g.Name,
				g.DisplayOrder,
				g.Items.Select(i => new OptionItemsResponse(
					i.Id,
					i.Name,
					i.Price,
					i.DisplayOrder,
					i.IsPobular
				)).ToList()
			)).ToList()
		);
	}
}