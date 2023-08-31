namespace Taskmaster.Models;

public class Project
{
	public Project()
	{
		Id = Guid.NewGuid();
	}

	public Guid Id { get; set; }

	public Guid UserId { get; set; }

	public string Name { get; set; } = null!;

	public DateTime CreatedAt { get; set; }

	public User User { get; set; } = null!;

	public List<Task> Tasks { get; set; } = new List<Task>();

	public DisplaySettings DisplaySettings { get; set; } = null!;
}
