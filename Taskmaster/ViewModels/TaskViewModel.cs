using System.ComponentModel.DataAnnotations;
using Taskmaster.Models.Enums;
using Taskmaster.ViewModels.Attributes;

namespace Taskmaster.ViewModels;

public class TaskViewModel
{
	public Guid? Id { get; set; }

	public Guid ProjectId { get; set; }

	[Display(Name = "Назва задачі")]
	[Required(ErrorMessage = "Введіть назву задачі")]
	[MaxLength(50, ErrorMessage = "Довжина назви задачі має бути не більше 50 символів")]
	public string Name { get; set; } = null!;

	[Display(Name = "Опис задачі")]
	[MaxLength(1000, ErrorMessage = "Довжина опису має бути не більше 1000 символів")]
	public string? Description { get; set; }

	[Display(Name = "Пріоритет")]
	[Required(ErrorMessage = "Оберіть пріоритет")]
	public PriorityLevel Priority { get; set; }

	[Display(Name = "Дата завершення")]
	[DataType(DataType.Date, ErrorMessage = "Введіть коректний формат дати (наприклад, ДД-ММ-РРРР)")]
	[DateInFutureOnly]
	[RequireTimeWithDate]
	public DateTime? EndDate { get; set; }

	[Display(Name = "Час завершення")]
	[DataType(DataType.Time, ErrorMessage = "Введіть коректний формат часу (наприклад, ГГ:ХХ)")]
	[TimeInFutureOnly]
	public DateTime? EndTime { get; set; }
}
