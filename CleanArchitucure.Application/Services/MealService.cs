namespace CleanArchitucure.Application.Services;

public class MealService(IRepository<Meal> mealRepository) : IMealService
{
	private readonly IRepository<Meal> _mealRepository = mealRepository;

	public async Task<Result<Meal>> AddAsync(CreateMealRequest request, CancellationToken cancellationToken = default)
	{
		bool isMealExist = await _mealRepository
			.IsExistsAsync(m => m.Name == request.Name, cancellationToken: cancellationToken);

		if (isMealExist)
			return Result.Failure<Meal>(MealErrors.DoublicatedMealName);

		var meal = new Meal
		{
			Name = request.Name,
			Description = request.Description,
			Price = request.Price,
			MealOptionGroups = [.. request.Options.Select(o => new MealOptionGroup
			{
				Name = o.Name,
				DisplayOrder = o.DisplayOrder,
				Items = [.. o.Items.Select(oo => new OptionGroupItems
				{
					Name = oo.Name,
					Price = oo.Price,
					DisplayOrder = oo.DisplayOrder,
					IsPobular = oo.IsPobular
				})]
			})]
		};

		Meal newMeal = await _mealRepository.AddAsync(meal, cancellationToken);
		await _mealRepository.SaveChangesAsync(cancellationToken);

		return Result.Success(newMeal);
	}

	public async Task<Result<MealResponse>> GetMeal(string mealId, CancellationToken cancellationToken = default)
	{
		bool isMealExist = await _mealRepository
			.IsExistsAsync(m => m.Id == mealId, cancellationToken: cancellationToken);

		if (!isMealExist)
			return Result.Failure<MealResponse>(MealErrors.MealNotFound);

		var spec = new MealByIdWithOptionsAndItemsSpec(mealId);

		var response = await _mealRepository.GetOneWithSelectAsync(spec, cancellationToken);

		return Result.Success(response!);
	}
}