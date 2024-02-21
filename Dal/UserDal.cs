using Microsoft.Data.Sqlite;
using software_engineering_devops_qa.Models;

namespace software_engineering_devops_qa.Dal;

public class UserDal
{
	public User? GetByUsername(string username)
	{
		using (var connection = new SqliteConnection("Data Source=lms.db"))
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
					var user = new User
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

	private static readonly string getSql = @"
SELECT
	u.user_id,
	u.username,
	u.password_hash,
	u.role
FROM user u
	";
}