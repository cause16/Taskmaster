using Taskmaster.Models;

namespace Taskmaster.Services.Interfaces;

public interface IProjectTaskCrudService
{
	IQueryable<User> GetUserByUsername(string userName);

	IQueryable<Project> GetProjectById(string userName, Guid projectId);

	IQueryable<Models.Task> GetTaskById(string userName, Guid projectId, Guid taskId);

	Task<bool> CreateProjectAsync(string userName, Project newProject);

	IQueryable<Project> GetProjects(string userName);

	Task<bool> UpdateProjectAsync(string userName, Guid projectId, Project updatedProject);

	Task<bool> DeleteProjectAsync(string userName, Guid projectId);

	Task<bool> CreateTaskAsync(string userName, Guid projectId, Models.Task newTask);

	IQueryable<Models.Task> GetTasks(string userName, Guid projectId);

	Task<bool> UpdateTaskAsync(string userName, Guid projectId, Guid taskId, Models.Task updatedTask);

	Task<bool> DeleteTaskAsync(string userName, Guid projectId, Guid taskId);

	IQueryable<DisplaySettings> GetDisplaySettings(string userName, Guid projectId);

	Task<bool> UpdateDisplaySettingsAsync(string userName, Guid projectId, DisplaySettings updatedDisplaySettings);
}
