using Taskmaster.Models.Enums;

namespace Taskmaster.Models;

public class Task
{
	public Task()
	{
		Id = Guid.NewGuid();
	}

	public Guid Id { get; set; }

	public Guid ProjectId { get; set; }

	public string Name { get; set; } = null!;

	public string? Description { get; set; }

	public PriorityLevel Priority { get; set; }

	public DateTime? EndDate { get; set; }

	public DateTime? EndTime { get; set; }

	public DateTime CreatedAt { get; set; }

	public Project Project { get; set; } = null!;
}
