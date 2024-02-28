using software_engineering_devops_qa.Models;
using software_engineering_devops_qa.Util;

namespace software_engineering_devops_qa.Dal;

public class EnrolmentDal : IDal<Enrolment>
{
	public static void Init(string dbConnection)
	{
		using var sqlite = new SqliteContext(dbConnection);
		sqlite.ExecuteNonQuery(initSql);
	}

	public List<Enrolment> Get(string dbConnection)
	{
		throw new NotImplementedException();
	}

	public int Add(string dbConnection, Enrolment model)
	{
		throw new NotImplementedException();
	}

	private static readonly string initSql = @"
CREATE TABLE IF NOT EXISTS enrolment
(
	enrolment_id INTEGER PRIMARY KEY AUTOINCREMENT,
	course_id INTEGER NOT NULL,
	user_id INTEGER NOT NULL,
	course_date DATETIME NOT NULL
);
";
}