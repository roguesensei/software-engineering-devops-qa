using System.Data;
using software_engineering_devops_qa.Models;
using software_engineering_devops_qa.Util;

namespace software_engineering_devops_qa.Dal;

public class UserDal : IDal<User>
{
	public static void Init(string dbConnection)
	{
		using var sqlite = new SqliteContext(dbConnection);
		sqlite.ExecuteNonQuery(initSql);
	}

	public List<User> Get(string dbConnection)
	{
		throw new NotImplementedException();
	}

	public User? GetByUsername(string dbConnection, string username)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ReadFirst(UserReader, $"{getSql} WHERE u.username = $username", [
			new("$username", username)
		]);

		// using (var connection = new SqliteConnection(Config.LmsDbConnection))
		// {
		// 	connection.Open();

		// 	var command = connection.CreateCommand();
		// 	command.CommandText = $"{getSql} WHERE u.username = $username";
		// 	command.Parameters.AddWithValue("$username", username);

		// 	using (var reader = command.ExecuteReader())
		// 	{
		// 		while(reader.Read())
		// 		{
		// 			var i = -1;
		// 			var user = new User()
		// 			{
		// 				UserId = reader.GetInt32(++i),
		// 				Username = reader.GetString(++i),
		// 				PasswordHash = reader.GetString(++i),
		// 				Role = (Role)reader.GetInt32(++i)
		// 			};

		// 			return user;
		// 		}
		// 	}
		// }

		// return null;
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
			//PasswordHash = dr.GetByte(++i),
			Role = (Role)dr.GetInt32(++i)
		};

		return user;
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
	u.role,
	u.password_hash
FROM user u
	";

	private static readonly string addSql = @"
	INSERT INTO user (username, password_hash, role)
	VALUES ($username, $password_hash, $role)
	";
}