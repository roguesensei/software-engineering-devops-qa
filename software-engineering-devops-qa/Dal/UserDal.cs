using System.Data;
using software_engineering_devops_qa.Models;
using software_engineering_devops_qa.Util;

namespace software_engineering_devops_qa.Dal;

public class UserDal : IDal<User>
{
	public static void Init(string dbConnection, string defaultAdminPassword)
	{
		using var sqlite = new SqliteContext(dbConnection);
		sqlite.ExecuteNonQuery(initSql);

		var adminUser = new UserDal().GetByUsername(dbConnection, "admin");
		if (adminUser is null) // Create default admin if one doesn't exist
		{
			var passwordHash = PasswordUtil.HashPassword(defaultAdminPassword);

			var newAdmin = new User
			{
				Username = "admin",
				PasswordHash = passwordHash,
				Role = Role.Admin
			};

			if (new UserDal().Add(dbConnection, newAdmin) == 0)
			{
				throw new Exception("Failed to create the admin user");
			}
		}
	}

	public List<User> Get(string dbConnection)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ReadAll(UserReader, getSql);
	}

	public User? GetById(string dbConnection, int userId)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ReadFirst(UserReader, $"{getSql} WHERE u.user_id = $user_id", [new("$user_id", userId)]);
	}

	public User? GetByUsername(string dbConnection, string username)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ReadFirst(UserReader, $"{getSql} WHERE u.username = $username", [new("$username", username)]);
	}

	public int Add(string dbConnection, User model)
	{
		using var sqlite = new SqliteContext(dbConnection);
		sqlite.ExecuteNonQuery(addSql, [
			new("$username", model.Username),
			new("$password_hash", SqlDbType.Binary) { Value = model.PasswordHash },
			new("$role", (int)model.Role)
		]);

		return sqlite.GetLastRowId();
	}

	private static User UserReader(IDataReader dr)
	{
		var i = -1;
		var user = new User()
		{
			UserId = dr.GetInt32(++i),
			Username = dr.GetString(++i),
			Role = (Role)dr.GetInt32(++i),
			PasswordHash = (byte[])dr.GetValue(++i)
		};

		return user;
	}

	public bool Update(string dbConnection, User model)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ExecuteNonQuery(updateSql, [
			new("$id", model.UserId),
			new("$role", model.Role)
		]) > 0;
	}

	public bool Delete(string dbConnection, int id)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ExecuteNonQuery(deleteSql, [new("$id", id)]) > 0;
	}

	private static readonly string initSql = @"
CREATE TABLE IF NOT EXISTS user
(
	user_id INTEGER PRIMARY KEY AUTOINCREMENT,
	username VARCHAR(50) NOT NULL,
	password_hash BLOB NOT NULL,
	role INTEGER NOT NULL DEFAULT(0)
)";

	private static readonly string getSql = @"
SELECT
	u.user_id,
	u.username,
	u.role,
	u.password_hash
FROM user u";

	private static readonly string addSql = @"
INSERT INTO user (username, password_hash, role)
VALUES ($username, $password_hash, $role)";

	private static readonly string updateSql = @"
UPDATE user
SET
	role = $role
WHERE user_id = $id
";

	private static readonly string deleteSql = @"
DELETE FROM user
WHERE user_id = $id
";
}