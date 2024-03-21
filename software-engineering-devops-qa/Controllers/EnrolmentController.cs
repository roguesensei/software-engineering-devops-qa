using Microsoft.AspNetCore.Mvc;
using software_engineering_devops_qa.Attributes;
using software_engineering_devops_qa.Dal;
using software_engineering_devops_qa.Models;

namespace software_engineering_devops_qa.Controllers;

public class EnrolmentController : BaseController
{
	[HttpGet("get")]
	public IActionResult Get() => Ok(new EnrolmentDal()
		.Get(Config.LmsDbConnection, CurrentObject!.UserId, CurrentObject.Role));


	[AdminOnly]
	[HttpPost("add")]
	public IActionResult Add([FromBody] Enrolment model) => Ok(new EnrolmentDal()
		.Add(Config.LmsDbConnection, model));

	[AdminOnly]
	[HttpPost("update")]
	public IActionResult Update([FromBody] Enrolment model) => Ok(new EnrolmentDal()
		.Update(Config.LmsDbConnection, model));

	[AdminOnly]
	[HttpPost("delete")]
	public IActionResult Delete([FromBody] int id) => Ok(new EnrolmentDal()
		.Delete(Config.LmsDbConnection, id));
}