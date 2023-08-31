using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Taskmaster.Models.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.Property(t => t.Id)
			   .ValueGeneratedNever();

		builder.Property(u => u.FirstName)
			   .HasMaxLength(50)
			   .IsRequired();

		builder.Property(u => u.LastName)
			   .HasMaxLength(50)
			   .IsRequired();
	}
}
