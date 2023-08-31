using System.ComponentModel.DataAnnotations;

namespace Taskmaster.ViewModels;

public class ProjectViewModel
{
	public Guid? Id { get; set; }

    [Display(Name = "Назва проєкту")]
	[Required(ErrorMessage = "Введіть назву проєкту")]
	[MaxLength(50, ErrorMessage = "Довжина назви проєкту має бути не більше 50 символів")]
	public string Name { get; set; } = null!;
}
