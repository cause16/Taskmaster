using Microsoft.AspNetCore.Identity;

namespace Taskmaster.Models;

public class User : IdentityUser<Guid>
{
	public User()
	{
		Id = Guid.NewGuid();
		SecurityStamp = Guid.NewGuid().ToString();
	}

	public string FirstName { get; set; } = null!;

	public string LastName { get; set; } = null!;

	public List<Project> Projects { get; set; } = new List<Project>();
}
