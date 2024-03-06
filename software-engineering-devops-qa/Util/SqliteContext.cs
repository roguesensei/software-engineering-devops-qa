using System.Data;
using Microsoft.Data.Sqlite;

namespace software_engineering_devops_qa.Util;

// SQLite helper class
public class SqliteContext(string connectionString) : IDisposable
{
	private readonly SqliteConnection connection = new(connectionString);

	public void Dispose()
	{
		if (connection.State == ConnectionState.Open)
		{
			connection.Close();
		}
		connection.Dispose();
	}

	public int ExecuteNonQuery(string sqlCommand, params SqliteParameter[] parameters)
	{
		return CreateCommand(sqlCommand, parameters).ExecuteNonQuery();
	}

	public IDataReader ExecuteReader(string sqlCommand, params SqliteParameter[] parameters)
	{
		return CreateCommand(sqlCommand, parameters).ExecuteReader();
	}

	public List<T> ReadAll<T>(Func<IDataReader, T> instance, string sqlCommand, params SqliteParameter[] parameters)
	{
		List<T> items = [];

		using var reader = CreateCommand(sqlCommand, parameters).ExecuteReader();
		while(reader.Read())
		{
			items.Add(instance(reader));
		}

		return items;		
	}

	public T? ReadFirst<T>(Func<IDataReader, T> instance, string sqlCommand, params SqliteParameter[] parameters)
	{
		using var reader = CreateCommand(sqlCommand, parameters).ExecuteReader();
		if (reader.Read())
		{
			return instance(reader);
		}
		return default;
	}

	public int GetLastRowId()
	{
		return ReadFirst((dr) => (int)dr.GetDecimal(0), "select last_insert_rowid()");
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