using software_engineering_devops_qa.Models;
using software_engineering_devops_qa.Util;
using System.Data;

namespace software_engineering_devops_qa.Dal;

public class EnrolmentDal : IDal<Enrolment>
{
	public static void Init(string dbConnection)
	{
		using var sqlite = new SqliteContext(dbConnection);
		sqlite.ExecuteNonQuery(initSql);
	}

	public List<Enrolment> Get(string dbConnection, int userId, Role userRole)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ReadAll(EnrolmentReader, getSql, [
			new("$user_id", userId),
			new("$role", (int)userRole)
		]);
	}

	public int Add(string dbConnection, Enrolment model)
	{
		using var sqlite = new SqliteContext(dbConnection);
		sqlite.ExecuteNonQuery(addSql, [
			new("$user_id", model.UserId),
			new("$course_id", model.CourseId),
			new("$course_date", model.CourseDate)
		]);

		return sqlite.GetLastRowId();
	}

	public bool Update(string dbConnection, Enrolment model)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ExecuteNonQuery(updateSql, [
			new("$id", model.EnrolmentId),
			new("$user_id", model.UserId),
			new("$course_id", model.CourseId),
			new("$course_date", model.CourseDate)
		]) > 0;
	}

	public bool Delete(string dbConnection, int id)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ExecuteNonQuery(deleteSql, [new("$id", id)]) > 0;
	}

	private static Enrolment EnrolmentReader(IDataReader dr)
	{
		var i = -1;
		var enrolment = new Enrolment()
		{
			EnrolmentId = dr.GetInt32(++i),
			CourseId = dr.GetInt32(++i),
			UserId = dr.GetInt32(++i),
			CourseDate = dr.GetDateTime(++i)
		};

		return enrolment;
	}

	private static readonly string initSql = @"
CREATE TABLE IF NOT EXISTS enrolment
(
	enrolment_id INTEGER PRIMARY KEY AUTOINCREMENT,
	course_id INTEGER NOT NULL,
	user_id INTEGER NOT NULL,
	course_date DATETIME NOT NULL
);";

	private static readonly string getSql = @"
SELECT
	e.enrolment_id,
	e.course_id,
	e.user_id,
	e.course_date
FROM enrolment e
WHERE e.user_id = $user_id OR $role = 1
";

	private static readonly string addSql = @"
INSERT INTO enrolment (course_id, user_id, course_date)
VALUES ($course_id, $user_id, $course_date)
";

	private static readonly string updateSql = @"
UPDATE enrolment
SET
	course_id = $course_id,
	user_id = $user_id,
	course_date = $course_date
WHERE enrolment_id = $id
";

	private static readonly string deleteSql = @"
DELETE FROM enrolment
WHERE enrolment_id = $id
";
}