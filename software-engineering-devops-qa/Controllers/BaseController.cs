using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using software_engineering_devops_qa.Authentication;
using software_engineering_devops_qa.Models.Auth;

namespace software_engineering_devops_qa.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
	public ICurrentObject? CurrentObject => GetCurrentUser();

	private ICurrentObject? GetCurrentUser()
	{
		var idClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id");
		if (idClaim != null)
		{
			var id = int.Parse(idClaim.Value);

			return LmsAuthentication.GetCurrent(id);
		}

		return null;
	}
}