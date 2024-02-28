using Microsoft.Data.Sqlite;
using software_engineering_devops_qa.Models;
using software_engineering_devops_qa.Util;

namespace software_engineering_devops_qa.Dal;

public class UserDal : IDal<User>
{
	public static void Init()
	{
		using var sqlite = new SqliteContext(Config.LmsDbConnection);
		sqlite.ExecuteNonQuery(initSql);
	}

	public List<User> Get()
	{
		throw new NotImplementedException();
	}

	public User? GetByUsername(string username)
	{
		using (var connection = new SqliteConnection(Config.LmsDbConnection))
		{
			connection.Open();

			var command = connection.CreateCommand();
			command.CommandText = $"{getSql} WHERE u.username = $username";
			command.Parameters.AddWithValue("$username", username);

			using (var reader = command.ExecuteReader())
			{
				while(reader.Read())
				{
					var i = -1;
					var user = new User()
					{
						UserId = reader.GetInt32(++i),
						Username = reader.GetString(++i),
						PasswordHash = reader.GetString(++i),
						Role = (Role)reader.GetInt32(++i)
					};

					return user;
				}
			}
		}

		return null;
	}

	private static readonly string initSql = @"
CREATE TABLE IF NOT EXISTS user
(
	user_id INTEGER PRIMARY KEY AUTOINCREMENT,
	username VARCHAR(50) NOT NULL,
	password_hash BLOB NOT NULL,
	role INTEGER NOT NULL DEFAULT(0)
)
";

	private static readonly string getSql = @"
SELECT
	u.user_id,
	u.username,
	u.password_hash,
	u.role
FROM user u
	";
}