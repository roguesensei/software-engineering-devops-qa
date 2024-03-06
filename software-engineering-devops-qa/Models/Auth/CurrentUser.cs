using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace software_engineering_devops_qa.Models.Auth;

public class CurrentUser : ICurrentObject
{
	public int UserId { get; set; }

	public required string Username { get; set; }

	public Role Role { get; set; } = Role.Student;

	[JsonIgnore]
	public byte[]? PasswordHash { get; set; }

	public List<Claim> GetClaims()
	{
		return
		[
			new("id", UserId.ToString())
		];
	}

	public bool ValidatePassword(string password)
	{
		if (PasswordHash == null)
		{
			return false;
		}

		var passwordHash = SHA256.HashData(Encoding.UTF8.GetBytes(password));

		return Encoding.UTF8.GetString(passwordHash) == Encoding.UTF8.GetString(PasswordHash);
	}
}