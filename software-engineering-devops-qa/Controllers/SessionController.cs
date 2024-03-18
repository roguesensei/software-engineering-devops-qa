using Microsoft.AspNetCore.Mvc;

namespace software_engineering_devops_qa.Controllers;

public class SessionController : BaseController
{
	[HttpGet("getCurrentUser")]
	public IActionResult GetCurrentUser() => Ok(CurrentObject);
}