using System.ComponentModel.DataAnnotations;

namespace Taskmaster.ViewModels;

public class LoginViewModel
{
	[Display(Name = "Логін")]
	[Required(ErrorMessage = "Введіть логін")]
	[MaxLength(100, ErrorMessage = "Довжина логіну має бути не більше 100 символів")]
	public string UserName { get; set; } = null!;

	[Display(Name = "Пароль")]
	[UIHint("Password")]
	[Required(ErrorMessage = "Введіть пароль")]
	[MinLength(8, ErrorMessage = "Довжина паролю має бути не менше 8 символів")]
	[MaxLength(100, ErrorMessage = "Довжина паролю має бути не більше 100 символів")]
	public string Password { get; set; } = null!;

	[Display(Name = "Запам'ятати мене")]
	public bool RememberMe { get; set; }
}
