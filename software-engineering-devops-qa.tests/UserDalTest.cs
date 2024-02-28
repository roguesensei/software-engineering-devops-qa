using System.Reflection;
using NUnit.Framework;
using software_engineering_devops_qa.Dal;

[TestFixture]
public class UserDalTest
{
	[SetUp]
	public void SetUp()
	{
		UserDal.Init();
	}

	[Test]
	public void Test()
	{
		Assert.IsFalse(true, "Failed the test test");
	}
}