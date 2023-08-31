using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Taskmaster.Models.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
	public void Configure(EntityTypeBuilder<Project> builder)
	{
		builder.ToTable("Projects")
			   .HasKey(p => p.Id);

		builder.HasOne(p => p.User)
			   .WithMany(u => u.Projects)
			   .HasForeignKey(p => p.UserId)
			   .OnDelete(DeleteBehavior.Cascade);

		builder.Property(p => p.Id)
			   .ValueGeneratedNever();

		builder.Property(p => p.Name)
			   .HasMaxLength(50)
			   .IsRequired();

		builder.Property(p => p.CreatedAt)
			   .IsRequired()
			   .HasDefaultValueSql("GETDATE()");

		builder.HasOne(p => p.DisplaySettings)
			   .WithOne(vs => vs.Project)
			   .HasForeignKey<DisplaySettings>(vs => vs.ProjectId);
	}
}
