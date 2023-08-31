using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Taskmaster.Models.EntityConfigurations;

public class DisplaySettingsConfiguration : IEntityTypeConfiguration<DisplaySettings>
{
	public void Configure(EntityTypeBuilder<DisplaySettings> builder)
	{
		builder.ToTable("DisplaySettings")
			   .HasKey(ds => ds.Id);

		builder.Property(ds => ds.Id)
			   .ValueGeneratedNever();

		builder.Property(ds => ds.DisplayOrder)
			   .IsRequired();

		builder.Property(ds => ds.SortBy)
			   .IsRequired();
	}
}
