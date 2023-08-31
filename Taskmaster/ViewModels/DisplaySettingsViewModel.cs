using System.ComponentModel.DataAnnotations;
using Taskmaster.Models.Enums;

namespace Taskmaster.ViewModels;

public class DisplaySettingsViewModel
{
	public Guid ProjectId { get; set; }

	[Display(Name = "Порядок відображення")]
	[Required(ErrorMessage = "Оберіть порядок відобарження")]
	public TaskDisplayOrder DisplayOrder { get; set; }

	[Display(Name = "Сортування")]
	[Required(ErrorMessage = "Оберіть сортування")]
	public TaskSortOption SortBy { get; set; }
}
