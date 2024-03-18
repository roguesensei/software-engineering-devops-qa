using Microsoft.AspNetCore.Mvc;
using software_engineering_devops_qa.Attributes;
using software_engineering_devops_qa.Dal;

namespace software_engineering_devops_qa.Controllers;

public class EnrolmentController : BaseController
{
	[HttpGet("get")]
	public IActionResult Get() => Ok(new EnrolmentDal().Get(Config.LmsDbConnection));

	[AdminOnly]
	[HttpPost("add")]
	public IActionResult Add() => Ok();
}