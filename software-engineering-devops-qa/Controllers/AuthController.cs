using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using software_engineering_devops_qa.Authentication;
using software_engineering_devops_qa.Models.PostModels;

namespace software_engineering_devops_qa.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginModel model)
	{
		var currObj = LmsAuthentication.GetUserByUsername(model.Username);

		if (currObj != null)
		{
			var identity = new ClaimsIdentity(currObj.GetClaims(), CookieAuthenticationDefaults.AuthenticationScheme);

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme, 
				new ClaimsPrincipal(identity), 
				new AuthenticationProperties { IsPersistent = true }
			);

			return Ok();
		}

		return Unauthorized();
	}
}