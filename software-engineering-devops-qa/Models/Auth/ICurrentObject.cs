using System.Security.Claims;
using System.Text.Json.Serialization;

namespace software_engineering_devops_qa.Models.Auth;

public interface ICurrentObject
{
	int UserId { get; set; }

	string Username { get; set; }

	Role Role { get; set; }

	[JsonIgnore]
	byte[]? PasswordHash { get; set; }

	List<Claim> GetClaims();

	bool ValidatePassword(string password);
}