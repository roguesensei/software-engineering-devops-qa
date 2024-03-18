using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using software_engineering_devops_qa.Authentication;
using software_engineering_devops_qa.Dal;
using software_engineering_devops_qa.Models;
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

		return Unauthorized("Invalid username or password (Hint: usernames and passwords are CaSE SEnSitiVE)");
	}

	[HttpPost("register")]
	public IActionResult Register([FromBody] LoginModel model)
	{
		if (new UserDal().GetByUsername(Config.LmsDbConnection, model.Username) != null)
		{
			return BadRequest("User with this username already exists");
		}
		else if (!PasswordUtil.FitsPasswordPolicy(model.Password))
		{
			return BadRequest(PasswordUtil.passwordPolicyError);
		}

		var newUser = new User
		{
			Username = model.Username,
			PasswordHash = PasswordUtil.HashPassword(model.Password)
		};

		if (new UserDal().Add(Config.LmsDbConnection, newUser) != 0)
		{
			var currObj = LmsAuthentication.GetUserByUsername(model.Username);
			if (currObj != null)
			{
				var jwt = JwtUtil.CreateJwt(DateTime.Now.AddHours(1), currObj.GetClaims());
				return Ok(jwt);
			}
		}

		return Unauthorized("User could not be created, please try again");
	}
}