using System.ComponentModel.DataAnnotations;

namespace Taskmaster.ViewModels.Attributes;

public class TimeInFutureOnlyAttribute : ValidationAttribute
{
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		if (value is DateTime time)
		{
			if (validationContext.ObjectInstance is TaskViewModel model)
			{
				if (model.EndDate == null)
					return ValidationResult.Success;

				if (model.EndDate.Value.Date == DateTime.Now.Date && time.TimeOfDay < DateTime.Now.TimeOfDay)
					return new ValidationResult("Виберіть час, який не буде меншим, ніж поточний");
			}
		}

		return ValidationResult.Success;
	}
}
