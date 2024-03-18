using Microsoft.AspNetCore.Mvc;
using software_engineering_devops_qa.Dal;

namespace software_engineering_devops_qa.Controllers;

public class UserController : BaseController
{
	[HttpGet("get")]
	public IActionResult Get() => Ok(new UserDal().Get(Config.LmsDbConnection));
}