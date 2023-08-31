namespace Taskmaster.Models;

public class CompanyDetails
{
	public CompanyDetails()
	{
		Id = Guid.NewGuid();
	}

	public Guid Id { get; set; }

	public string Name { get; set; } = null!;

	public string? Phone { get; set; }

	public string? Email { get; set; }

	public string? Country { get; set; }

	public string? City { get; set; }

	public string? Street { get; set; }

	public string? PostIndex { get; set; }
}
