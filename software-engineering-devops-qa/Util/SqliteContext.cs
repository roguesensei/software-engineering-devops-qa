using Microsoft.Data.Sqlite;

namespace software_engineering_devops_qa.Util;

// SQLite helper class
public class SqliteContext(string connectionString) : IDisposable
{
	private readonly SqliteConnection connection = new(connectionString);

	public void Dispose()
	{
		connection.Close();
	}

	public int ExecuteNonQuery(string sqlCommand, params SqliteParameter[] parameters)
	{
		var command = CreateCommand(sqlCommand, parameters);
		return command.ExecuteNonQuery();
	}

	private SqliteCommand CreateCommand(string sqlCommand, params SqliteParameter[] parameters)
	{
		connection.Open();

		var command = connection.CreateCommand();
		command.CommandText = sqlCommand;
		command.Parameters.AddRange(parameters);

		return command;
	}
}