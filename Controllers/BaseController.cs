using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace software_engineering_devops_qa.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class BaseController : ControllerBase
{

}