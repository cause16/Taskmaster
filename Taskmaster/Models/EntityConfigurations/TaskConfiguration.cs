using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Taskmaster.Models.EntityConfigurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
	public void Configure(EntityTypeBuilder<Task> builder)
	{
		builder.ToTable("Tasks")
			   .HasKey(t => t.Id);

		builder.HasOne(t => t.Project)
			   .WithMany(p => p.Tasks)
			   .HasForeignKey(t => t.ProjectId)
			   .OnDelete(DeleteBehavior.Cascade);

		builder.Property(t => t.Id)
			   .ValueGeneratedNever();

		builder.Property(t => t.Name)
			   .HasMaxLength(50)
			   .IsRequired();

		builder.Property(t => t.Description)
			   .HasMaxLength(1000)
			   .IsRequired(false);

		builder.Property(t => t.Priority)
			   .IsRequired();

		builder.Property(t => t.EndDate)
			   .IsRequired(false);

		builder.Property(t => t.EndTime)
			   .IsRequired(false);

		builder.Property(p => p.CreatedAt)
			   .IsRequired()
			   .HasDefaultValueSql("GETDATE()");
	}
}
