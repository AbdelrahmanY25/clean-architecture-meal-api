namespace CleanArchitucure.Application.Services;

public class MealService(IRepository<Meal> mealRepository) : IMealService
{
	private readonly IRepository<Meal> _mealRepository = mealRepository;

	public async Task<Result<MealResponse>> AddAsync(CreateMealRequest request, CancellationToken cancellationToken = default)
	{
		bool isMealExist = await _mealRepository
			.IsExistsAsync(m => m.Name == request.Name, cancellationToken: cancellationToken);

		if (isMealExist)
			return Result.Failure<MealResponse>(MealErrors.DoublicatedMealName);

		var meal = request.Adapt<Meal>();

		Meal newMeal = await _mealRepository.AddAsync(meal, cancellationToken);
		await _mealRepository.SaveChangesAsync(cancellationToken);

		return Result.Success(newMeal.Adapt<MealResponse>());
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

	public async Task<IReadOnlyCollection<MealResponse>> GetAll(CancellationToken cancellationToken = default)
	{
		var spec = new AllMealsWithOptionsAdnOptionItems();

		var response = await _mealRepository.GetAllWithSelectAsync(spec, cancellationToken);

		return response;
	}
}