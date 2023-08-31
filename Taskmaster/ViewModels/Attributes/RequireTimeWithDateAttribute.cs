using System.ComponentModel.DataAnnotations;

namespace Taskmaster.ViewModels.Attributes;

public class RequireTimeWithDateAttribute : ValidationAttribute
{
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		if (value is not DateTime)
		{
			if (validationContext.ObjectInstance is TaskViewModel model)
			{
				if (value == null && model.EndTime != null)
					return new ValidationResult("Виберіть дату");
			}
		}

		return ValidationResult.Success;
	}
}
