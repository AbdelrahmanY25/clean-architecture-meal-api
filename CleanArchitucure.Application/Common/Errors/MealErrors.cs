namespace CleanArchitucure.Application.Common.Errors;

public static class MealErrors
{
	public static readonly Error MealNotFound = 
		new("Meal.MealNotFound", "The meal was not found.", StatusCodes.Status404NotFound);

	public static readonly Error DoublicatedMealName = 
		new("Meal.DoublicatedMealName", "A meal with the same name already exists.", StatusCodes.Status409Conflict);
}
