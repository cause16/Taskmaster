using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Taskmaster.Models.EntityConfigurations;

public class CompanyDetailsConfiguration : IEntityTypeConfiguration<CompanyDetails>
{
	public void Configure(EntityTypeBuilder<CompanyDetails> builder)
	{
		builder.ToTable("CompanyDetails")
			   .HasKey(cd => cd.Id);

		builder.HasIndex(cd => cd.Name)
			   .IsUnique();

		builder.Property(cd => cd.Id)
			   .ValueGeneratedNever();

		builder.Property(cd => cd.Name)
			   .HasMaxLength(100)
			   .IsRequired();

		builder.Property(cd => cd.Phone)
			   .HasMaxLength(50)
			   .IsRequired(false);

		builder.Property(cd => cd.Email)
			   .HasMaxLength(100)
			   .IsRequired(false);

		builder.Property(cd => cd.Country)
			   .HasMaxLength(50)
			   .IsRequired(false);

		builder.Property(cd => cd.City)
			   .HasMaxLength(50)
			   .IsRequired(false);

		builder.Property(cd => cd.Street)
			   .HasMaxLength(100)
			   .IsRequired(false);

		builder.Property(cd => cd.PostIndex)
			   .HasMaxLength(50)
			   .IsRequired(false);
	}
}
