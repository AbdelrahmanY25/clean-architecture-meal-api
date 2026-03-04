namespace CleanArchitucure.Application;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddApplication()
		{
			services.AddFluentValidationConfig();

			services.AddServices();

			return services;
		}

		private IServiceCollection AddFluentValidationConfig()
		{
			services
				.AddFluentValidationAutoValidation()
				.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

			return services;
		}

		private IServiceCollection AddServices()
		{
			services.AddScoped<IMealService, MealService>();
			services.AddScoped<IMealOptionsService, MealOptionsService>();
			services.AddScoped<IOptionItemsService, OptionItemsService>();

			return services;
		}
	}
}