namespace software_engineering_devops_qa.Models.PostModels;

public class LoginModel
{
	public required string Username { get; set; }

	public required string Password { get; set; }

	public required string ClientId { get; set; }
}