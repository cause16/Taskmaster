using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taskmaster.Models.EntityConfigurations;

namespace Taskmaster.Models.Context;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) 
		: base(options)
	{
	}

	public DbSet<CompanyDetails> CompanyDetails { get; set; }

	public DbSet<Project> Projects { get; set; }

	public DbSet<Task> Tasks { get; set; }

	public DbSet<DisplaySettings> DisplaySettings { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new CompanyDetailsConfiguration());
		builder.ApplyConfiguration(new UserConfiguration());
		builder.ApplyConfiguration(new ProjectConfiguration());
		builder.ApplyConfiguration(new TaskConfiguration());
		builder.ApplyConfiguration(new DisplaySettingsConfiguration());
		base.OnModelCreating(builder);
	}
}
