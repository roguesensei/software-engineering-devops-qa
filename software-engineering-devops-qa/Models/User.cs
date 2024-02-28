using System.Text.Json.Serialization;

namespace software_engineering_devops_qa.Models;

public record User
{
	public int UserId { get; set; }

	public required string Username { get; init; }

	public Role Role { get; init; }

	[JsonIgnore]
	public byte[]? PasswordHash { get; set; }
}