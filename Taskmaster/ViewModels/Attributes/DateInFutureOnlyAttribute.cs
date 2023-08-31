using System.ComponentModel.DataAnnotations;

namespace Taskmaster.ViewModels.Attributes;

public class DateInFutureOnlyAttribute : ValidationAttribute
{
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		if (value is DateTime date)
		{
			if (date.Date < DateTime.Now.Date)
				return new ValidationResult("Виберіть дату, яка не буде меншою за поточну");
		}

		return ValidationResult.Success;
	}
}
