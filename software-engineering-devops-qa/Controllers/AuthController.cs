using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using software_engineering_devops_qa.Authentication;
using software_engineering_devops_qa.Models.PostModels;
using software_engineering_devops_qa.Util;

namespace software_engineering_devops_qa.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
	[HttpPost("login")]
	public IActionResult Login([FromBody] LoginModel model)
	{
		var currObj = LmsAuthentication.GetUserByUsername(model.Username);

		if (currObj != null && currObj.ValidatePassword(model.Password))
		{
			var jwt = JwtUtil.CreateJwt(DateTime.Now.AddHours(1), currObj.GetClaims());

			return Ok(jwt);
		}

		return Unauthorized("Invalid username or password");
	}
}