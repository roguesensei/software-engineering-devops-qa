using System.Text.Json.Serialization;

namespace software_engineering_devops_qa.Models;

public class User
{
	public int UserId { get; set; }

	public required string Username { get; set; }

	public Role Role { get; set; } = Role.Student;

	[JsonIgnore]
	public string? PasswordHash { get; set; }
}