using System.Security.Claims;

namespace software_engineering_devops_qa.Models.Auth;

public interface ICurrentObject
{
	int UserId { get; set; }

	string Username { get; set; }

	Role Role { get; set; }

	List<Claim> GetClaims();
}