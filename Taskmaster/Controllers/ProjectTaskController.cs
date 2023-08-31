using AutoMapper;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Taskmaster.Models;
using Taskmaster.Services.Interfaces;
using Taskmaster.ViewModels;

namespace Taskmaster.Controllers;

[Route("project-task")]
[Authorize]
public class ProjectTaskController : Controller
{
	private readonly IProjectTaskCrudService _projectTaskCrudService;
	private readonly IMapper _mapper;
	private readonly ITaskSortService _taskSortService;

	public ProjectTaskController(IProjectTaskCrudService projectTaskService, IMapper mapper, ITaskSortService taskSortService)
	{
		_projectTaskCrudService = projectTaskService;
		_mapper = mapper;
		_taskSortService = taskSortService;
	}

	[HttpGet("{action=index}")]
	public async Task<IActionResult> Index()
	{
		string userName = HttpContext.User.Identity?.Name ?? "";
		var projects = await _projectTaskCrudService.GetProjects(userName).ToListAsync();

		var model = _mapper.Map<IEnumerable<ProjectViewModel>>(projects);

		return View(model);
	}

	[HttpGet("project/{id}")]
	public async Task<IActionResult> Project(Guid id)
	{
		string userName = HttpContext.User.Identity?.Name ?? "";
		var projects = await _projectTaskCrudService.GetProjects(userName).ToListAsync();
		var selectedProject = await _projectTaskCrudService.GetProjectById(userName, id)
			.Include(p => p.Tasks)
			.Include(p => p.DisplaySettings)
			.FirstOrDefaultAsync();

		var model = _mapper.Map<IEnumerable<ProjectViewModel>>(projects);

		if (selectedProject == null)
			return NotFound();

		var tasks = selectedProject.Tasks;
		var displaySettings = selectedProject.DisplaySettings;

		var sortedTasks = _taskSortService.SortTasks(tasks, displaySettings.SortBy, displaySettings.DisplayOrder);
		ViewBag.Tasks = _mapper.Map<IEnumerable<TaskViewModel>>(sortedTasks);
		ViewBag.Culture = new CultureInfo("uk-UA");

		ViewBag.SelectedProjectId = id;
		ViewBag.SelectedProjectName = selectedProject.Name;

		return View("Tasks", model);
	}

	[HttpGet("create-project")]
	public IActionResult CreateProject()
	{
		var model = new ProjectViewModel();

		return View(model);
	}

	[HttpPost("create-project")]
	public async Task<IActionResult> CreateProject(ProjectViewModel model)
	{
		if (ModelState.IsValid)
		{
			string userName = HttpContext.User.Identity?.Name ?? "";
			var newProject = _mapper.Map<Project>(model);

			newProject.DisplaySettings = new DisplaySettings()
			{
				DisplayOrder = Models.Enums.TaskDisplayOrder.Ascending,
				SortBy = Models.Enums.TaskSortOption.Default
			};

			bool result = await _projectTaskCrudService.CreateProjectAsync(userName, newProject);

			return Json(new { Success = result, newProject.Id });
		}

		return View(model);
	}

	[HttpGet("update-project/{id}")]
	public async Task<IActionResult> UpdateProject(Guid id)
	{
		string userName = HttpContext.User.Identity?.Name ?? "";
		var project = await _projectTaskCrudService.GetProjectById(userName, id).FirstOrDefaultAsync();

		if (project == null)
			return NotFound();

		var model = _mapper.Map<ProjectViewModel>(project);

		return View(model);
	}

	[HttpPut("update-project")]
	public async Task<IActionResult> UpdateProject(ProjectViewModel model)
	{
		if (ModelState.IsValid)
		{
			string userName = HttpContext.User.Identity?.Name ?? "";
			var updatedProject = _mapper.Map<Project>(model);
			bool result = await _projectTaskCrudService.UpdateProjectAsync(userName, model.Id ?? default, updatedProject);

			return Json(new { Success = result });
		}

		return View(model);
	}

	[HttpGet("delete-project/{id}")]
	public IActionResult DeleteProject(Guid id)
	{
		var model = new ProjectViewModel { Id = id };

		return View(model);
	}

	[HttpDelete("delete-project")]
	public async Task<IActionResult> DeleteProject(ProjectViewModel model)
	{
		string userName = HttpContext.User.Identity?.Name ?? "";
		bool result = await _projectTaskCrudService.DeleteProjectAsync(userName, model.Id ?? default);

		return Json(new { Success = result });
	}

	[HttpGet("create-task/{id}")]
	public IActionResult CreateTask(Guid id)
	{
		var model = new TaskViewModel { ProjectId = id };

		return View(model);
	}

	[HttpPost("create-task")]
	public async Task<IActionResult> CreateTask(TaskViewModel model)
	{
		if (ModelState.IsValid)
		{
			string userName = HttpContext.User.Identity?.Name ?? "";
			var newTask = _mapper.Map<Models.Task>(model);
			bool result = await _projectTaskCrudService.CreateTaskAsync(userName, model.ProjectId, newTask);

			return Json(new { Success = result });
		}

		return View(model);
	}

	[HttpGet("update-task/{id}")]
	public async Task<IActionResult> UpdateTask(Guid id, Guid projectId)
	{
		string userName = HttpContext.User.Identity?.Name ?? "";
		var task = await _projectTaskCrudService.GetTaskById(userName, projectId, id).FirstOrDefaultAsync();

		if (task == null)
			return NotFound();

		var model = _mapper.Map<TaskViewModel>(task);

		return View(model);
	}

	[HttpPut("update-task")]
	public async Task<IActionResult> UpdateTask(TaskViewModel model)
	{
		if (ModelState.IsValid)
		{
			string userName = HttpContext.User.Identity?.Name ?? "";
			var updatedTask = _mapper.Map<Models.Task>(model);
			bool result = await _projectTaskCrudService.UpdateTaskAsync(userName, model.ProjectId, model.Id ?? default, updatedTask);

			return Json(new { Success = result });
		}

		return View(model);
	}

	[HttpGet("delete-task/{id}")]
	public IActionResult DeleteTask(Guid id, Guid projectId)
	{
		var model = new TaskViewModel { Id = id, ProjectId = projectId };

		return View(model);
	}

	[HttpDelete("delete-task/{id?}")]
	public async Task<IActionResult> DeleteTask(TaskViewModel model)
	{
		string userName = HttpContext.User.Identity?.Name ?? "";
		bool result = await _projectTaskCrudService.DeleteTaskAsync(userName, model.ProjectId, model.Id ?? default);

		return Json(new { Success = result });
	}

	[HttpGet("update-display-settings/{id}")]
	public async Task<IActionResult> UpdateDisplaySettings(Guid id)
	{
		string userName = HttpContext.User.Identity?.Name ?? "";
		var displaySettings = await _projectTaskCrudService.GetDisplaySettings(userName, id).FirstOrDefaultAsync();

		if (displaySettings == null)
			return NotFound();

		var model = _mapper.Map<DisplaySettingsViewModel>(displaySettings);

		return View(model);
	}

	[HttpPut("update-display-settings")]
	public async Task<IActionResult> UpdateDisplaySettings(DisplaySettingsViewModel model)
	{
		if (ModelState.IsValid)
		{
			string userName = HttpContext.User.Identity?.Name ?? "";
			var updatedDisplaySettings = _mapper.Map<DisplaySettings>(model);
			bool result = await _projectTaskCrudService.UpdateDisplaySettingsAsync(userName, model.ProjectId, updatedDisplaySettings);

			return Json(new { Success = result });
		}

		return View(model);
	}

	[HttpGet("get-anti-forgery-token")]
	public IActionResult GetAntiForgeryToken([FromServices] IAntiforgery antiforgery)
	{
		var tokens = antiforgery.GetAndStoreTokens(HttpContext);

		return Ok(new { tokens.RequestToken });
	}
}
