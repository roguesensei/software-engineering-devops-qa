using Microsoft.AspNetCore.Mvc;
using software_engineering_devops_qa.Attributes;
using software_engineering_devops_qa.Dal;
using software_engineering_devops_qa.Models;

namespace software_engineering_devops_qa.Controllers;

public class CourseController : BaseController
{
	[HttpGet("get")]
	public IActionResult Get() => Ok(new CourseDal()
		.Get(Config.LmsDbConnection, CurrentObject!.UserId, CurrentObject.Role));

	[AdminOnly]
	[HttpPost("add")]
	public IActionResult Add([FromBody] Course model) => Ok(new CourseDal()
		.Add(Config.LmsDbConnection, model));

	[AdminOnly]
	[HttpPost("update")]
	public IActionResult Update([FromBody] Course model) => Ok(new CourseDal()
		.Update(Config.LmsDbConnection, model));

	[AdminOnly]
	[HttpPost("delete")]
	public IActionResult Delete([FromBody] int id) => Ok(new CourseDal()
		.Delete(Config.LmsDbConnection, id));
}