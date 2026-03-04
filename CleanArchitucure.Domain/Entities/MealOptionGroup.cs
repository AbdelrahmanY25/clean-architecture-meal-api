namespace CleanArchitucure.Domain.Entities;

public class MealOptionGroup
{
	public string Id { get; set; } = Guid.CreateVersion7().ToString();
	public string MealId { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;
	public int DisplayOrder { get; set; }

	public Meal Meal { get; set; } = default!;
	public ICollection<OptionGroupItems> Items { get; set; } = [];
}