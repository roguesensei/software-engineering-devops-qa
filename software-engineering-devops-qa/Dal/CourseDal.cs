using software_engineering_devops_qa.Models;
using software_engineering_devops_qa.Util;
using System.Data;

namespace software_engineering_devops_qa.Dal;

public class CourseDal : IDal<Course>
{
	public static void Init(string dbConnection)
	{
		using var sqlite = new SqliteContext(dbConnection);
		sqlite.ExecuteNonQuery(initSql);
	}

	public List<Course> Get(string dbConnection)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ReadAll(CourseReader, getSql);
	}

	public int Add(string dbConnection, Course model)
	{
		using var sqlite = new SqliteContext(dbConnection);
		sqlite.ExecuteNonQuery(addSql, [
			new("$name", model.Name),
			new("$description", (object?)model.Description ?? DBNull.Value),
			new("$instructor_id", model.InstructorId)
		]);

		return sqlite.GetLastRowId();
	}

	public bool Update(string dbConnection, Course model)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ExecuteNonQuery(updateSql, [
			new("$id", model.CourseId),
			new("$name", model.Name),
			new("$description", (object?)model.Description ?? DBNull.Value),
			new("$instructor_id", model.InstructorId)
		]) > 0;
	}

	public bool Delete(string dbConnection, int id)
	{
		using var sqlite = new SqliteContext(dbConnection);
		return sqlite.ExecuteNonQuery(deleteSql, [new("$id", id)]) > 0;
	}

	private static Course CourseReader(IDataReader dr)
	{
		var i = -1;
		var course = new Course()
		{
			CourseId = dr.GetInt32(++i),
			Name = dr.GetString(++i),
			InstructorId = dr.GetInt32(++i),
			Description = dr.IsDBNull(++i) ? null : dr.GetString(i)
		};

		return course;
	}

	private static readonly string initSql = @"
CREATE TABLE IF NOT EXISTS course
(
	course_id INTEGER PRIMARY KEY AUTOINCREMENT,
	name VARCHAR(200) NOT NULL,
	description VARCHAR(500) NULL,
	instructor_id INTEGER NOT NULL
);";

	private static readonly string getSql = @"
SELECT
	c.course_id,
	c.name,
	c.instructor_id,
	c.description
FROM course c
";

	private static readonly string addSql = @"
INSERT INTO course (name, description, instructor_id)
VALUES ($name, $description, $instructor_id)
";

	private static readonly string updateSql = @"
UPDATE course
SET
	name = $name,
	description = $description,
	instructor_id = $instructor_id
WHERE course_id = $id
";

	private static readonly string deleteSql = @"
DELETE FROM course
WHERE course_id = $id
";
}
