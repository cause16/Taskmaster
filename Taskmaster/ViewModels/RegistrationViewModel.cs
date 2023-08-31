using System.ComponentModel.DataAnnotations;

namespace Taskmaster.ViewModels;

public class RegistrationViewModel
{
	[Display(Name = "Ім'я")]
	[Required(ErrorMessage = "Введіть ім'я")]
	[MaxLength(50, ErrorMessage = "Довжина імені має бути не більше 50 символів")]
	public string FirstName { get; set; } = null!;

	[Display(Name = "Прізвище")]
	[Required(ErrorMessage = "Введіть прізвище")]
	[MaxLength(50, ErrorMessage = "Довжина прізвища має бути не більше 50 символів")]
	public string LastName { get; set; } = null!;

	[Display(Name = "Email")]
	[UIHint("EmailAddress")]
	[Required(ErrorMessage = "Введіть email")]
	[MaxLength(100, ErrorMessage = "Довжина email має бути не більше 100 символів")]
	public string Email { get; set; } = null!;

	[Display(Name = "Пароль")]
	[UIHint("Password")]
	[Required(ErrorMessage = "Введіть пароль")]
	[MinLength(8, ErrorMessage = "Довжина паролю має бути не менше 8 символів")]
	[MaxLength(100, ErrorMessage = "Довжина паролю має бути не більше 100 символів")]
	public string Password { get; set; } = null!;

	[Display(Name = "Введіть пароль ще раз")]
	[UIHint("Password")]
	[Required(ErrorMessage = "Введіть пароль")]
	[MaxLength(100, ErrorMessage = "Довжина паролю має бути не більше 100 символів")]
	[Compare(nameof(Password), ErrorMessage = "Паролі не співпадають")]
	public string ConfirmPassword { get; set; } = null!;
}
