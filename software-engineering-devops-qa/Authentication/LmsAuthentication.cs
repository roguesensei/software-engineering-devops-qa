using software_engineering_devops_qa.Dal;
using software_engineering_devops_qa.Models.Auth;

namespace software_engineering_devops_qa.Authentication;

public class LmsAuthentication
{
	private static Dictionary<int, ICurrentObject> Users { get; set; } = new();

	public static ICurrentObject GetCurrent(int userId)
	{
		if (Users.ContainsKey(userId))
		{
			return Users[userId];
		}

		return GetCurrentUser(userId);
	}

	public static CurrentUser? GetUserByUsername(string username)
	{
		var user = new UserDal().GetByUsername(username);
		if (user is not null)
		{
			return GetCurrentUser(user.UserId);
		}

		return null;
	}

	private static CurrentUser GetCurrentUser(int userId)
	{
		return new() { UserId = userId, Username = "" };
	}
}