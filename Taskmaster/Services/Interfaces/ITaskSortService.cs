using Taskmaster.Models.Enums;

namespace Taskmaster.Services.Interfaces;

public interface ITaskSortService
{
	List<Models.Task> SortTasks(List<Models.Task> tasks, TaskSortOption taskSortOption, TaskDisplayOrder taskDisplayOrder);
}
