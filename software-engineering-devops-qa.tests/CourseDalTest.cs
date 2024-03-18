using software_engineering_devops_qa.Dal;
using software_engineering_devops_qa.Models;

namespace software_engineering_devops_qa.Tests;

[TestFixture]
public class CourseDalTest
{
	private const string dbConnection = $"Data Source=.course-dal-test.db";

	[SetUp]
	public void SetUp()
	{
		CourseDal.Init(dbConnection);
	}

	[Test]
	public void AddCourse_Test()
	{
		var course = new Course()
		{
			Name = "C#",
			InstructorId = 1
		};
		course.CourseId = new CourseDal().Add(dbConnection, course);

		Assert.That(course.CourseId, Is.Not.EqualTo(0), "Course was not created");
	}

	[Test]
	public void UpdateCourse_Test()
	{
		var dal = new CourseDal();
		var updateName = "reactjs";
		var course = new Course()
		{
			Name = "C#",
			InstructorId = 1
		};

		course = new()
		{
			CourseId = dal.Add(dbConnection, course),
			Name = updateName,
			InstructorId = 1
		};
		dal.Update(dbConnection, course);

		var compare = dal.Get(dbConnection).FirstOrDefault(x => x.CourseId == course.CourseId);

		Assert.That(course.Name, Is.EqualTo(compare?.Name), "Course was not updated, the name comparison failed");
	}

	[Test]
	public void DeleteCourse_Test()
	{
		var dal = new CourseDal();
		var course = new Course()
		{
			Name = "C#",
			InstructorId = 1
		};
		var id = dal.Add(dbConnection, course);

		Assert.That(dal.Delete(dbConnection, id), "Could not delete the course");
	}
}