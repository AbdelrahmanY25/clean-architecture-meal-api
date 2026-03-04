namespace CleanArchitucure.Infrastructure.Persistence.Configurations;

internal class MealOptionGroupsConfigurations : IEntityTypeConfiguration<MealOptionGroup>
{
	public void Configure(EntityTypeBuilder<MealOptionGroup> builder)
	{
		builder.HasMany(o => o.Items)
			.WithOne(i => i.OptionGroup)
			.HasForeignKey(i => i.OptionGroupId);

		builder.Property(e => e.Name)
			.HasMaxLength(30);
	}
}