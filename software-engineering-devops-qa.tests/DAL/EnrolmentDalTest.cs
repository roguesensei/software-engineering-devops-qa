using software_engineering_devops_qa.Dal;
using software_engineering_devops_qa.Models;

namespace software_engineering_devops_qa.Tests.DAL;

[TestFixture]
public class EnrolmentDalTest
{
	private const string dbConnection = $"Data Source=.enrolment-dal-test.db";

	[SetUp]
	public void SetUp()
	{
		EnrolmentDal.Init(dbConnection);
	}

	[Test]
	public void AddEnrolment_Test()
	{
		var enrolment = new Enrolment()
		{
			UserId = 1,
			CourseId = 1,
			CourseDate = DateTime.Now
		};
		enrolment.EnrolmentId = new EnrolmentDal().Add(dbConnection, enrolment);

		Assert.That(enrolment.CourseId, Is.Not.EqualTo(0), "Course was not created");
	}

	[Test]
	public void UpdateEnrolment_Test()
	{
		var dal = new EnrolmentDal();
		var enrolment = new Enrolment()
		{
			UserId = 1,
			CourseId = 1,
			CourseDate = DateTime.Now
		};

		enrolment = new()
		{
			EnrolmentId = dal.Add(dbConnection, enrolment),
			UserId = 1,
			CourseId = 1,
			CourseDate = DateTime.Now
		};
		dal.Update(dbConnection, enrolment);

		var compare = dal.Get(dbConnection).FirstOrDefault(x => x.EnrolmentId == enrolment.EnrolmentId);
		Assert.That(enrolment.CourseDate, Is.EqualTo(compare?.CourseDate), "Enrolment was not updated, the name comparison failed");
	}

	[Test]
	public void DeleteEnrolment_Test()
	{
		var dal = new EnrolmentDal();
		var enrolment = new Enrolment()
		{
			UserId = 1,
			CourseId = 1,
			CourseDate = DateTime.Now
		};
		var id = dal.Add(dbConnection, enrolment);

		Assert.That(dal.Delete(dbConnection, id), "Could not delete the enrolment");
	}
}