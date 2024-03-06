using software_engineering_devops_qa.Dal;
using software_engineering_devops_qa.Models.Auth;

namespace software_engineering_devops_qa.Authentication;

public class LmsAuthentication
{
	private static Dictionary<int, ICurrentObject> Users { get; set; } = new();

	public static ICurrentObject GetCurrent(int userId)
	{
		if (Users.TryGetValue(userId, out var user))
		{
			return user;
		}

		return GetCurrentUser(userId);
	}

	public static CurrentUser? GetUserByUsername(string username)
	{
		var user = new UserDal().GetByUsername(Config.LmsDbConnection, username);
		if (user is not null)
		{
			return GetCurrentUser(user.UserId);
		}

		return null;
	}

	private static CurrentUser GetCurrentUser(int userId)
	{
		var user = new UserDal().GetById(Config.LmsDbConnection, userId)!;
		return new() 
		{ 
			UserId = user.UserId,
			Username = user.Username,
			Role = user.Role,
			PasswordHash = user.PasswordHash
		};
	}
}