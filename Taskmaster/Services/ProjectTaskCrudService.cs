using Taskmaster.Services.Interfaces;
using Taskmaster.Models;
using Taskmaster.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Taskmaster.Services;

public class ProjectTaskCrudService : IProjectTaskCrudService
{
	private readonly AppDbContext _context;

	public ProjectTaskCrudService(AppDbContext context)
	{
		_context = context;
	}

	public IQueryable<User> GetUserByUsername(string userName)
	{
		var user = _context.Users
			.Where(u => u.UserName == userName);

		return user;
	}

	public IQueryable<Project> GetProjectById(string userName, Guid projectId)
	{
		var project = GetUserByUsername(userName)
			.Include(u => u.Projects)
			.SelectMany(u => u.Projects)
			.Where(p => p.Id == projectId);

		return project;
	}

	public IQueryable<Models.Task> GetTaskById(string userName, Guid projectId, Guid taskId)
	{
		var task = GetProjectById(userName, projectId)
			.Include(p => p.Tasks)
			.SelectMany(p => p.Tasks)
			.Where(t => t.Id == taskId);

		return task;
	}

	public async Task<bool> CreateProjectAsync(string userName, Project newProject)
	{
		var currentUser = await GetUserByUsername(userName)
			.Include(u => u.Projects)
			.FirstOrDefaultAsync();

		if (currentUser == null)
			return false;

		currentUser.Projects.Add(newProject);

		int result = await _context.SaveChangesAsync();

		return result > 0;
	}

	public IQueryable<Project> GetProjects(string userName)
	{
		var projects = GetUserByUsername(userName)
			.Include(u => u.Projects)
			.SelectMany(u => u.Projects)
			.OrderBy(p => p.CreatedAt);

		return projects;
	}

	public async Task<bool> UpdateProjectAsync(string userName, Guid projectId, Project updatedProject)
	{
		int result = await GetProjectById(userName, projectId)
			.ExecuteUpdateAsync(s => s.SetProperty(p => p.Name, p => updatedProject.Name));

		return result > 0;
	}

	public async Task<bool> DeleteProjectAsync(string userName, Guid projectId)
	{
		int result = await GetProjectById(userName, projectId)
			.ExecuteDeleteAsync();

		return result > 0;
	}

	public async Task<bool> CreateTaskAsync(string userName, Guid projectId, Models.Task newTask)
	{
		var currentProject = await GetProjectById(userName, projectId)
			.Include(p => p.Tasks)
			.FirstOrDefaultAsync();

		if (currentProject == null)
			return false;

		currentProject.Tasks.Add(newTask);

		int result = await _context.SaveChangesAsync();

		return result > 0;
	}

	public IQueryable<Models.Task> GetTasks(string userName, Guid projectId)
	{
		var tasks = GetProjectById(userName, projectId)
			.Include(p => p.Tasks)
			.SelectMany(p => p.Tasks);

		return tasks;
	}

	public async Task<bool> UpdateTaskAsync(string userName, Guid projectId, Guid taskId, Models.Task updatedTask)
	{
		int result = await GetTaskById(userName, projectId, taskId)
			.ExecuteUpdateAsync(s => s
					.SetProperty(t => t.Name, t => updatedTask.Name)
					.SetProperty(t => t.Description, t => updatedTask.Description)
					.SetProperty(t => t.Priority, t => updatedTask.Priority)
					.SetProperty(t => t.EndDate, t => updatedTask.EndDate)
					.SetProperty(t => t.EndTime, t => updatedTask.EndTime));

		return result > 0;
	}

	public async Task<bool> DeleteTaskAsync(string userName, Guid projectId, Guid taskId)
	{
		int result = await GetTaskById(userName, projectId, taskId)
			.ExecuteDeleteAsync();

		return result > 0;
	}

	public IQueryable<DisplaySettings> GetDisplaySettings(string userName, Guid projectId)
	{
		var displaySettings = GetProjectById(userName, projectId)
			.Include(p => p.DisplaySettings)
			.Select(p => p.DisplaySettings);

		return displaySettings;
	}

	public async Task<bool> UpdateDisplaySettingsAsync(string userName, Guid projectId, DisplaySettings updatedDisplaySettings)
	{
		int result = await GetDisplaySettings(userName, projectId)
			.ExecuteUpdateAsync(s => s
					.SetProperty(ds => ds.DisplayOrder, ds => updatedDisplaySettings.DisplayOrder)
					.SetProperty(ds => ds.SortBy, ds => updatedDisplaySettings.SortBy));

		return result > 0;
	}
}
