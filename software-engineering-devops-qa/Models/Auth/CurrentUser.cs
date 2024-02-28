using System.Security.Claims;

namespace software_engineering_devops_qa.Models.Auth;

public class CurrentUser : ICurrentObject
{
	public int UserId { get; set; }

	public required string Username { get; set; }

	public Role Role { get; set; } = Role.Student;

	public List<Claim> GetClaims()
	{
		return
		[
			new("id", UserId.ToString())
		];
	}
}