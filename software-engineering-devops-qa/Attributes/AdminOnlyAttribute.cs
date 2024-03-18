using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using software_engineering_devops_qa.Models;
using Identity = software_engineering_devops_qa.Util.Idenity;

namespace software_engineering_devops_qa.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AdminOnlyAttribute : Attribute, IAuthorizationFilter
{
	public void OnAuthorization(AuthorizationFilterContext context)
	{
		if (!context.HttpContext.User.HasClaim(Identity.roleClaimName, ((int)Role.Admin).ToString()))
		{
			context.Result = new ForbidResult();
		}
	}
}