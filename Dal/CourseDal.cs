using software_engineering_devops_qa.Models;
using software_engineering_devops_qa.Util;

namespace software_engineering_devops_qa.Dal;

public class CourseDal : IDal<Course>
{
	public static void Init()
	{
		using var sqlite = new SqliteContext(Config.LmsDbConnection);
		sqlite.ExecuteNonQuery(initSql);
	}

	public List<Course> Get()
	{
		throw new NotImplementedException();
	}

	private static readonly string initSql = @"
	CREATE TABLE IF NOT EXISTS course
(
	course_id INTEGER PRIMARY KEY AUTOINCREMENT,
	name VARCHAR(200) NOT NULL,
	description VARCHAR(500) NULL,
	instructor_id INTEGER NOT NULL
);
	";
}
