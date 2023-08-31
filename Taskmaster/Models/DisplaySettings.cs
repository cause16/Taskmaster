using Taskmaster.Models.Enums;

namespace Taskmaster.Models;

public class DisplaySettings
{
    public DisplaySettings()
    {
		Id = Guid.NewGuid();
	}

    public Guid Id { get; set; }

	public Guid ProjectId { get; set; }

	public TaskDisplayOrder DisplayOrder { get; set; }

	public TaskSortOption SortBy { get; set; }

	public Project Project { get; set; } = null!;
}
