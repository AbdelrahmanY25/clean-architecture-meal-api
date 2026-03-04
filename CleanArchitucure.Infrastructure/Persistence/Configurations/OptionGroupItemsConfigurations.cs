namespace CleanArchitucure.Infrastructure.Persistence.Configurations;

internal class OptionGroupItemsConfigurations : IEntityTypeConfiguration<OptionGroupItems>
{
	public void Configure(EntityTypeBuilder<OptionGroupItems> builder)
	{
		builder
			.Property(e => e.Name)
			.HasMaxLength(30);	

		builder
			.Property(e => e.Price)
			.HasPrecision(5,2);
	}
}