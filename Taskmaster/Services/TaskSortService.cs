using Taskmaster.Models.Enums;
using Taskmaster.Services.Interfaces;

namespace Taskmaster.Services;

public class TaskSortService : ITaskSortService
{
	public List<Models.Task> SortTasks(List<Models.Task> tasks, TaskSortOption taskSortOption, TaskDisplayOrder taskDisplayOrder)
	{
		switch (taskSortOption)
		{
			case TaskSortOption.Default:
				if (taskDisplayOrder == TaskDisplayOrder.Ascending)
				{
					return tasks.OrderBy(t => t.CreatedAt)
								.ToList();
				}
				else
				{
					return tasks.OrderByDescending(t => t.CreatedAt)
								.ToList();
				}
			case TaskSortOption.Name:
				if (taskDisplayOrder == TaskDisplayOrder.Ascending)
				{
					return tasks.OrderBy(t => t.Name)
								.ThenBy(t => t.CreatedAt)
								.ToList();
				}
				else
				{
					return tasks.OrderByDescending(t => t.Name)
								.ThenByDescending(t => t.CreatedAt)
								.ToList();
				}
			case TaskSortOption.Priority:
				if (taskDisplayOrder == TaskDisplayOrder.Ascending)
				{
					return tasks.OrderBy(t => t.Priority)
								.ThenBy(t => t.CreatedAt)
								.ToList();
				}
				else
				{
					return tasks.OrderByDescending(t => t.Priority)
								.ThenByDescending(t => t.CreatedAt)
								.ToList();
				}
			case TaskSortOption.DueDate:
				if (taskDisplayOrder == TaskDisplayOrder.Ascending)
				{
					return tasks.OrderBy(t => t.EndDate)
								.ThenBy(t => t.EndTime)
								.ThenBy(t => t.CreatedAt)
								.ToList();
				}
				else
				{
					return tasks.OrderByDescending(t => t.EndDate)
								.ThenByDescending(t => t.EndTime)
								.ThenByDescending(t => t.CreatedAt)
								.ToList();
				}
			default:
				return tasks;
		}
	}
}
