using Microsoft.AspNetCore.Mvc;

namespace software_engineering_devops_qa.Controllers;

public class EnrolmentController : BaseController
{
	[HttpGet("get")]
	public IActionResult Get() => Ok();
}